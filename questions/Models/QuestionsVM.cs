using questions.Data.Models;

namespace questions.Models
{
    public class QuestionsVM
    {
        public QuestionsVM()
        {
            this.Questions = new List<QUESTION>();
        }
        public long? parentId { get; set; }
        public List<QUESTION> Questions { get; set; }

    }
}
