using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questions.Data;
using questions.Data.Models;
using questions.Models;

namespace questions.Controllers
{
    public class TakeExamController : Controller
    {
        private readonly ILogger<QuestionsController> logger;
        private readonly ApplicationDbContext context;
        
        public TakeExamController(ILogger<QuestionsController> _logger
            , ApplicationDbContext _context
            )
        {
            logger = _logger;
            context = _context;
           
        }
        public async Task<IActionResult> Index()
        {
            TakeExamVM myModel = new TakeExamVM();
            myModel.AvailableExams = await this.context.ExamRepositories.ToListAsync();
            return View(myModel);
        }

        public async Task<IActionResult> Take(long? id)
        {
            var ids = await this.context.Questions.Where(q => q.REPO_ID == id).Select(q => q.ID).ToListAsync();
            var count = ids.Count();
            

            System.Random rand = new System.Random(DateTime.Now.Millisecond);

            var questionList = new List<long?>();
            if (count > 20)
            {
                for (int i = 0; i < 20; i++)
                {
                    var randomIndex = rand.Next(count);
                    questionList.Add(randomIndex);
                }
            }
            else
            {
                questionList = ids;
            }

            //create exam and add questions
            EXAM exam = new EXAM();
            exam.REPO_ID = id;

            await this.context.Exams.AddAsync(exam);
            await this.context.SaveChangesAsync();
            foreach (var item in questionList)
            {
                EXAM_QUETION question = new EXAM_QUETION();
                question.EXAM_ID = exam.ID;
                question.IS_ANSWERED = false;
                await this.context.ExamQuestions.AddAsync(question);
                
            }
            await this.context.SaveChangesAsync();

            return RedirectToAction("Exam", new { id=exam.ID });

        }


        public async Task<IActionResult> Exam(long? id)
        {
            var exam = await this.context.Exams.Where(q => q.ID == id).FirstOrDefaultAsync();
            if(exam == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var examRepo  = await this.context.ExamRepositories.Where(q => q.ID == exam.REPO_ID).FirstOrDefaultAsync();
            ViewBag.ExamName = examRepo.NAME;
            var currentQuestion = await this.context.ExamQuestions.Where(e => e.IS_ANSWERED == false).OrderBy(e => e.ID).FirstOrDefaultAsync();
            if(currentQuestion == null)
            {
                return RedirectToAction("ExamSummary");
            }
            currentQuestion.Question = await this.context.Questions.Where(q => q.ID == currentQuestion.QUESTION_ID).FirstOrDefaultAsync();
            currentQuestion.Question.Selections = await this.context.Selections.Where(q => q.QUESTION_ID == currentQuestion.QUESTION_ID).OrderBy(o=>o.ID).ToListAsync();
            return View(currentQuestion);

        }
    }
}
