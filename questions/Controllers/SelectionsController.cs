using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questions.Data;
using questions.Data.Models;
using questions.Models;
using System.Runtime.CompilerServices;

namespace questions.Controllers
{

    public class SelectionsController : Controller
    {
        private readonly ILogger<QuestionsController> logger;
        private readonly ApplicationDbContext context;
        
        public SelectionsController(ILogger<QuestionsController> _logger
            , ApplicationDbContext _context
            )
        {
            logger = _logger;
            context = _context;
           
            
        }

       

        [HttpGet]
        public async Task<IActionResult> Selection(long? parentId,long? id = null)
        {
            bool checkedParent = await checkParent(parentId);
            if(!checkedParent)
            {
                return RedirectToAction("Index", "ExamRepo");
            }
            var parent = await this.context.Questions.Where(w => w.ID == parentId).FirstOrDefaultAsync();
            ViewBag.id = id;
            SelectionsPostVM myModel = new SelectionsPostVM();
            
            myModel.RepoID = parent.REPO_ID;
            myModel.QuestionID = parent.ID;
            
            if (id != null)
            {
                
                var oldSelection = await context.Selections.Where(w => w.ID == id).FirstOrDefaultAsync();
                if (oldSelection != null)
                {
                    myModel.SelectionID = oldSelection.ID;
                    myModel.Selection = oldSelection.SELECTION_TEXT;
                    

                }
            }

            return View(myModel);
        }

        private async Task<bool> checkParent(long? parentId)
        {
            if (parentId == null)
            {
                TempData["message"] = "Question not found!";
                TempData["class"] = "alert alert-danger";
                return false;
            }
            var parent = await context.Questions.Where(w => w.ID == parentId).FirstOrDefaultAsync();
            if (parent == null)
            {
                TempData["message"] = "Question not found!";
                TempData["class"] = "alert alert-danger";
                return false;
            }
            return true;
        }

        


        [HttpPost]
        public async Task<IActionResult> Selection( SelectionsPostVM myModel)
        {
           
            if (ModelState.IsValid)
            {
                
                myModel.Selection = (myModel.Selection ?? "").Trim();
                
                bool checkedParent = await checkParent(myModel.QuestionID);
                if (!checkedParent)
                {
                    return RedirectToAction("Index", "ExamRepo");
                }
                var parent = await this.context.Questions.Where(w => w.ID == myModel.QuestionID).FirstOrDefaultAsync();
                myModel.RepoID = parent.REPO_ID;
                var id = myModel.SelectionID;
                ViewBag.id = id;

                if (id == null)
                {
                    //new model
                    var oldSelection = await context.Selections.Where(w => w.QUESTION_ID == myModel.QuestionID && w.SELECTION_TEXT == myModel.Selection).FirstOrDefaultAsync();
                    if (oldSelection != null)
                    {
                        ModelState.AddModelError("", "Selection already found before!");

                    }
                  
                    if (ModelState.IsValid)
                    {

                        SELECTION dbModel = new SELECTION();
                        dbModel.QUESTION_ID = myModel.QuestionID;
                        dbModel.SELECTION_TEXT = myModel.Selection;
                        
                        await context.AddAsync(dbModel);
                        await context.SaveChangesAsync();
                        TempData["message"] = "Added Successfully!";
                        TempData["class"] = "alert alert-success";
                        return RedirectToAction("Question", new { parentId = parent.REPO_ID, id = myModel.QuestionID });

                    }
                    else
                    {
                        return View(myModel);
                    }
                }
                else
                {
                    //edit model
                    var oldSelection = await context.Selections.Where(w => w.ID == id).FirstOrDefaultAsync();
                    if (oldSelection == null)
                    {
                        ModelState.AddModelError("", "Selection not found!");

                    }
                  
                    if (ModelState.IsValid)
                    {
                        oldSelection.SELECTION_TEXT = myModel.Selection;
                        
                        await context.SaveChangesAsync();
                        TempData["message"] = "Saved Successfully!";
                        TempData["class"] = "alert alert-success";
                        return RedirectToAction("Question", new { parentId = parent.REPO_ID , id=oldSelection.QUESTION_ID });

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


        [HttpPost]
        public async Task<IActionResult> Delete(DeleteVM myModel)
        {
            if (ModelState.IsValid)
            {
                var id = myModel.id;
                ViewBag.id = id;

                if (id == null)
                {

                    TempData["message"] = "Selection not found to delete!";
                    TempData["class"] = "alert alert-danger";
                    return RedirectToAction("Index", "ExamRepo");


                }
                else
                {
                    //delete model
                    var oldSeleciton = await context.Selections.Where(w => w.ID == id).FirstOrDefaultAsync();
                    if (oldSeleciton == null)
                    {
                        TempData["message"] = "Selection not found to delete!";
                        TempData["class"] = "alert alert-danger";
                        return RedirectToAction("Index", "ExamRepo");

                    }
                    var parentId = oldSeleciton.QUESTION_ID;
                    var parent = await this.context.Questions.Where(w => w.ID == parentId ).FirstOrDefaultAsync();
                    var repoId = parent.REPO_ID;
                    
                    context.Selections.Remove(oldSeleciton);
                    await context.SaveChangesAsync();
                    TempData["message"] = "Deleted Successfully!";
                    TempData["class"] = "alert alert-success";
                    return RedirectToAction("Index","Questions", new {id=parentId , parentId = repoId });



                }

            }
            else
            {
                return View(myModel);
            }
        }

        

        
    }
}
