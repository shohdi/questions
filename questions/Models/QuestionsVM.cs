using questions.Data.Models;

namespace questions.Models
{
    public class QuestionsVM
    {
        public QuestionsVM()
        {
            this.Questions = new List<QUESTION>();
        }
        public List<QUESTION> Questions { get; set; }

    }
}
