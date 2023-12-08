using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questions.Data;
using questions.Data.Models;
using questions.Models;
using System.Runtime.CompilerServices;

namespace questions.Controllers
{

    public class ExamRepoController : Controller
    {
        private readonly ILogger<ExamRepoController> logger;
        private readonly ApplicationDbContext context;
        public ExamRepoController(ILogger<ExamRepoController> _logger
            , ApplicationDbContext _context)
        {
            logger = _logger;
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var myModel = new ExamRepoVM();
            //read exams
            myModel.AllExams = await context.ExamRepositories.ToListAsync();

            return View(myModel);
        }

        [HttpGet]
        public async Task<IActionResult> Repository(long? RepoId = null)
        {
            ViewBag.RepoId = RepoId;
            ExamRepoPostVM myModel = new ExamRepoPostVM();
            if (RepoId != null)
            {
                var oldExam = await context.ExamRepositories.Where(w => w.ID == RepoId).FirstOrDefaultAsync();
                if (oldExam != null)
                {
                    myModel.Name = oldExam.NAME;
                    
                }
            }

            return View(myModel);
        }

        [HttpPost]
        public async Task<IActionResult> Repository( ExamRepoPostVM myModel)
        {
           
            if (ModelState.IsValid)
            {
                var RepoId = myModel.RepoId;
                ViewBag.RepoId = RepoId;

                if (RepoId == null)
                {
                    //new model
                    var oldExam = await context.ExamRepositories.Where(w => w.NAME == myModel.Name).FirstOrDefaultAsync();
                    if (oldExam != null)
                    {
                        ModelState.AddModelError("", "Exam already found before!");

                    }
                    if (ModelState.IsValid)
                    {
                        EXAM_REPOSITORY dbModel = new EXAM_REPOSITORY();
                        dbModel.NAME = myModel.Name;
                        await context.AddAsync(dbModel);
                        await context.SaveChangesAsync();
                        TempData["message"] = "Added Successfully!";
                        TempData["class"] = "alert alert-success";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        return View(myModel);
                    }
                }
                else
                {
                    //edit model
                    var oldExam = await context.ExamRepositories.Where(w => w.ID == RepoId).FirstOrDefaultAsync();
                    if (oldExam == null)
                    {
                        ModelState.AddModelError("", "Exam not found!");

                    }
                    if (ModelState.IsValid)
                    {
                        oldExam.NAME = myModel.Name;
                        await context.SaveChangesAsync();
                        TempData["message"] = "Saved Successfully!";
                        TempData["class"] = "alert alert-success";
                        return RedirectToAction("Index");

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
