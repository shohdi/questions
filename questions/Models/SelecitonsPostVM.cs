using questions.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace questions.Models
{
    public class SelectionsPostVM
    {


        [Required]
        [MaxLength(1000)]


        public string Selection { get; set; }

        public bool? IsAnswer { get; set; }

        public long? SelectionID { get; set; }

        [Required]
        public long? QuestionID { get; set; }


        [Required]
        public long? RepoID { get; set; }

    }

    
}
