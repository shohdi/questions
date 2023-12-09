using System.ComponentModel.DataAnnotations;

namespace questions.Models
{
    public class QuestionsPostVM
    {
        [Required]
        [MaxLength(450)]

        
        public string Question { get; set; }


        public long? QuestionId { get; set; }

        [Required]
        public long? RepoId { get; set; }

    }

    public class DeleteQuestionsVM
    {
        [Required]
        public long? QuestionId { get; set; }
    }
}
