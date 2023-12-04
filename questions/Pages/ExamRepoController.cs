using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questions.Data;
using questions.Models;

namespace questions.Pages
{
	
	public class ExamRepoController : Controller
	{
        private readonly ILogger<ExamRepoController> logger;
		private readonly ApplicationDbContext context;
        public ExamRepoController(ILogger<ExamRepoController> _logger
			,ApplicationDbContext _context) {
			this.logger = _logger;
			this.context = _context;
		}
        
        public async Task<IActionResult> Index()
		{
			var myModel = new ExamRepoVM();
			//read exams
			myModel.AllExams =  await this.context.ExamRepositories.ToListAsync();

			return View(myModel);
		}
	}
}
