using questions.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace questions.Models
{
    public class QuestionsPostVM
    {
        public QuestionsPostVM() {
            Selections = new List<SELECTION>();
        }

        [Required]
        [MaxLength(2000)]

        
        public string Question { get; set; }

        public string ImagePath { get; set; }


        public long? QuestionId { get; set; }

        [Required]
        public long? RepoId { get; set; }

        public List<SELECTION> Selections { get; set; } 

    }

    public class DeleteQuestionsVM
    {
        [Required]
        public long? QuestionId { get; set; }
    }
}
