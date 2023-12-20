using questions.Data.Models;

namespace questions.Models
{
    public class TakeExamVM
    {
        public TakeExamVM()
        {
            AvailableExams = new List<EXAM_REPOSITORY>();
        }
        public List<EXAM_REPOSITORY> AvailableExams { get; set; }
    }
}
