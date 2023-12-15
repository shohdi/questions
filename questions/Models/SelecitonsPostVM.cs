using questions.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace questions.Models
{
    public class SelectionsPostVM
    {
        public SelectionsPostVM ()
        {
            this.IsAnswer = false;

        }


        [Required]
        [MaxLength(1000)]


        public string Selection { get; set; }

        [Required]
        public bool IsAnswer { get; set; }

        public long? SelectionID { get; set; }

        [Required]
        public long? QuestionID { get; set; }


        [Required]
        public long? RepoID { get; set; }

    }

    
}
