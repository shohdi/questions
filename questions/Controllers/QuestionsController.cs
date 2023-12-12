using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questions.Data;
using questions.Data.Models;
using questions.Models;
using System.Runtime.CompilerServices;

namespace questions.Controllers
{

    public class QuestionsController : Controller
    {
        private readonly ILogger<QuestionsController> logger;
        private readonly ApplicationDbContext context;
        public QuestionsController(ILogger<QuestionsController> _logger
            , ApplicationDbContext _context)
        {
            logger = _logger;
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(long? parentId)
        {
            bool checkedParent = await checkParent(parentId);
            if (!checkedParent)
            {
                return RedirectToAction("Index", "ExamRepo");
            }
            var myModel = new QuestionsVM();
            myModel.parentId = parentId;
            //read Questions
            myModel.Questions = await context.Questions.Where(w=>w.REPO_ID == parentId).Include(a=>a.Selections).ToListAsync();

            return View(myModel);
        }

        [HttpGet]
        public async Task<IActionResult> Question(long? parentId,long? id = null)
        {
            bool checkedParent = await checkParent(parentId);
            if(!checkedParent)
            {
                return RedirectToAction("Index", "ExamRepo");
            }
            ViewBag.id = id;
            QuestionsPostVM myModel = new QuestionsPostVM();
            myModel.RepoId = parentId;
            if (id != null)
            {
                
                var oldQuestion = await context.Questions.Where(w => w.ID == id).Include(w=>w.Selections).FirstOrDefaultAsync();
                if (oldQuestion != null)
                {
                    myModel.QuestionId = oldQuestion.ID;
                    myModel.Question = oldQuestion.QUESTION_TEXT;
                    myModel.ImagePath = oldQuestion.IMAGE_PATH;
                    myModel.Selections = oldQuestion.Selections.ToList();

                }
            }

            return View(myModel);
        }

        private async Task<bool> checkParent(long? RepoId)
        {
            if (RepoId == null)
            {
                TempData["message"] = "Exam Repository not found to show its questions!";
                TempData["class"] = "alert alert-danger";
                return false;
            }
            var parent = await context.ExamRepositories.Where(w => w.ID == RepoId).FirstOrDefaultAsync();
            if (parent == null)
            {
                TempData["message"] = "Exam Repository not found to show its questions!";
                TempData["class"] = "alert alert-danger";
                return false;
            }
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteVM myModel)
        {
            if (ModelState.IsValid)
            {
                var id = myModel.id;
                ViewBag.id = id;

                if (id == null)
                {

                    TempData["message"] = "Question not found to delete!";
                    TempData["class"] = "alert alert-danger";
                    return RedirectToAction("Index","ExamRepo");


                }
                else
                {
                    //delete model
                    var oldQuestion = await context.Questions.Where(w => w.ID == id).FirstOrDefaultAsync();
                    if (oldQuestion == null)
                    {
                        TempData["message"] = "Question not found to delete!";
                        TempData["class"] = "alert alert-danger";
                        return RedirectToAction("Index","ExamRepo");

                    }
                    var parentId = oldQuestion.REPO_ID;

                    context.Questions.Remove(oldQuestion);    
                    await context.SaveChangesAsync();
                    TempData["message"] = "Deleted Successfully!";
                    TempData["class"] = "alert alert-success";
                    return RedirectToAction("Index",new {parentId= parentId });

                   

                }

            }
            else
            {
                return View(myModel);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Question( QuestionsPostVM myModel)
        {
           
            if (ModelState.IsValid)
            {
                bool checkedParent = await checkParent(myModel.RepoId);
                if (!checkedParent)
                {
                    return RedirectToAction("Index", "ExamRepo");
                }
                var id = myModel.QuestionId;
                ViewBag.id = id;

                if (id == null)
                {
                    //new model
                    var oldQuestion = await context.Questions.Where(w => w.REPO_ID == myModel.RepoId &&  w.QUESTION_TEXT == myModel.Question).FirstOrDefaultAsync();
                    if (oldQuestion != null)
                    {
                        ModelState.AddModelError("", "Question already found before!");

                    }
                    if (Request.Form.Files != null && Request.Form.Files.Count > 0)
                    {
                        var fileName = Request.Form.Files[0].FileName;
                        var length = Request.Form.Files[0].Length;
                        //byte[] btFile = 
                        //var fileBytes= Request.Form.Files[0].OpenReadStream().ReadAsync()
                        //if (Request.Form.Files[0].FileName.ToLower().EndsWith(""))
                    }
                    if (ModelState.IsValid)
                    {
                        
                        QUESTION dbModel = new QUESTION();
                        dbModel.REPO_ID = myModel.RepoId;
                        dbModel.QUESTION_TEXT = myModel.Question;
                        await context.AddAsync(dbModel);
                        await context.SaveChangesAsync();
                        TempData["message"] = "Added Successfully!";
                        TempData["class"] = "alert alert-success";
                        return RedirectToAction("Question", new { parentId = dbModel.REPO_ID, id = dbModel.ID });

                    }
                    else
                    {
                        return View(myModel);
                    }
                }
                else
                {
                    //edit model
                    var oldQuestion = await context.Questions.Where(w => w.ID == id).FirstOrDefaultAsync();
                    if (oldQuestion == null)
                    {
                        ModelState.AddModelError("", "Question not found!");

                    }
                    if (ModelState.IsValid)
                    {
                        oldQuestion.QUESTION_TEXT = myModel.Question;
                        await context.SaveChangesAsync();
                        TempData["message"] = "Saved Successfully!";
                        TempData["class"] = "alert alert-success";
                        return RedirectToAction("Question", new { parentId = oldQuestion.REPO_ID , id=oldQuestion.ID });

                    }
                    else
                    {
                        return View(myModel);
                    }

                }

            }
            else
            {
                return View(myModel);
            }
           

        }

    }
}
