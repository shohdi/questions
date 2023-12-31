﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IWebHostEnvironment environment;
        public QuestionsController(ILogger<QuestionsController> _logger
            , ApplicationDbContext _context
            , IWebHostEnvironment _environment)
        {
            logger = _logger;
            context = _context;
            this.environment = _environment;
            
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
                
                var oldQuestion = await context.Questions.Where(w => w.ID == id).FirstOrDefaultAsync();
                if (oldQuestion != null)
                {
                    myModel.QuestionId = oldQuestion.ID;
                    myModel.Question = oldQuestion.QUESTION_TEXT;
                    myModel.ImagePath = oldQuestion.IMAGE_PATH;
                    myModel.Selections = await context.Selections.Where(s=>s.QUESTION_ID == oldQuestion.ID).ToListAsync() ;

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
                    deleteImagePath(oldQuestion);
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
                
                myModel.Question = (myModel.Question ?? "").Trim();
                
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
                    var oldQuestion = await context.Questions.Where(w => w.REPO_ID == myModel.RepoId && w.QUESTION_TEXT == myModel.Question).FirstOrDefaultAsync();
                    if (oldQuestion != null)
                    {
                        ModelState.AddModelError("", "Question already found before!");

                    }
                    var btFile =  await getFile(myModel);
                    if (ModelState.IsValid)
                    {

                        QUESTION dbModel = new QUESTION();
                        dbModel.REPO_ID = myModel.RepoId;
                        dbModel.QUESTION_TEXT = myModel.Question;
                        if (btFile != null)
                        {
                            await saveFile(btFile, dbModel);
                        }
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
                    var btFile = await getFile(myModel);
                    if (ModelState.IsValid)
                    {
                        oldQuestion.QUESTION_TEXT = myModel.Question;
                        if(btFile != null)
                        {
                            await saveFile(btFile, oldQuestion);
                        }
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

        private async Task saveFile(byte[] btFile, QUESTION oldQuestion)
        {
            string myVirPath = Path.Combine("AppData", "Images", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
            string fileName = System.Guid.NewGuid().ToString() + ".jpg";
            string directory = Path.Combine(this.environment.WebRootPath, myVirPath);
            string fullFileName = Path.Combine(directory, fileName);
            string fullVirtualPath = Path.Combine(myVirPath, fileName);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            await System.IO.File.WriteAllBytesAsync(fullFileName, btFile);

            deleteImagePath(oldQuestion);
            oldQuestion.IMAGE_PATH = fullVirtualPath;




        }

        private void deleteImagePath(QUESTION oldQuestion)
        {
            if (!string.IsNullOrWhiteSpace(oldQuestion.IMAGE_PATH))
            {
                var oldFullFileName = Path.Combine(this.environment.WebRootPath, oldQuestion.IMAGE_PATH);
                try
                {
                    System.IO.File.Delete(oldFullFileName);
                }
                catch (Exception ex)
                {
                    this.logger.Log(LogLevel.Error, ex.ToString());

                }

            }
        }

        private async Task<byte[]> getFile(QuestionsPostVM myModel)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var fileData = Request.Form.Files[0];
                var fileName = fileData.FileName;
                var length = fileData.Length;
                bool isValidFile = true;
                if (!fileName.ToLower().EndsWith(".jpg"))
                {

                    ModelState.AddModelError("ImagePath", "only .jpg is allowed!");
                    isValidFile = false;
                }
                if (isValidFile)
                {
                   
                    byte[] btFile = new byte[1024];
                    List<byte> lstFile = new List<byte>();
                    int readed = 1;
                    var stream = fileData.OpenReadStream();
                    while (readed > 0)
                    {
                        btFile = new byte[1024];
                        readed = await stream.ReadAsync(btFile, 0, 1024);
                        if (readed == 1024)
                        {
                            lstFile.AddRange(btFile);
                        }
                        else if (readed > 0)
                        {
                            lstFile.AddRange(btFile.Take(readed));
                        }

                    }
                    btFile = lstFile.ToArray();
                    return btFile;
                }




            }
            return null;
        }
    }
}
