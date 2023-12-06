using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questions.Data;
using questions.Models;
using System.Runtime.CompilerServices;

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

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var myModel = new ExamRepoVM();
			//read exams
			myModel.AllExams =  await this.context.ExamRepositories.ToListAsync();

			return View(myModel);
		}

        [HttpGet]
        public async Task<IActionResult> Repository(long? RepoId = null)
		{

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Repository([FromBody] ExamRepoPostVM myModel,[FromQuery] long? RepoId = null)
        {
			if(ModelState.IsValid)
			{
				//var oldExam = this.Repository
			}
			else
			{
                return View(myModel);
            }
			return null;
            
        }

    }
}
