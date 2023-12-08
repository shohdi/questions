using System.ComponentModel.DataAnnotations;

namespace questions.Models
{
    public class ExamRepoPostVM
    {
        [Required]
        [MaxLength(450)]

        
        public string Name { get; set; }


        public long? RepoId { get; set; }

    }
}
