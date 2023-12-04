using questions.Data.Models;

namespace questions.Models
{
    public class ExamRepoVM
    {
        public ExamRepoVM()
        {
            this.AllExams = new List<EXAM_REPOSITORY>();
        }
        public List<EXAM_REPOSITORY> AllExams { get; set; }

    }
}
