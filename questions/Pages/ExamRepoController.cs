using Microsoft.AspNetCore.Mvc;

namespace questions.Pages
{
	
	public class ExamRepoController : Controller
	{
        
        public IActionResult Index()
		{
			return View();
		}
	}
}
