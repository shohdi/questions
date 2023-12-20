using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questions.Data;
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
    }
}
