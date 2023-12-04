using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace questions.Data.Models
{
    [Table("EXAMS")]
  

    public class EXAM
    {
        [Key]
        [Required]
        [Column(TypeName = "decimal(10)")]
        public long? ID { get; set; }

        



        [Required]
        [ForeignKey(nameof(EXAM_REPOSITORY))]
        [Column(TypeName = "decimal(10)")]
        public long? REPO_ID { get; set; }


        public EXAM_REPOSITORY? ExamRepository { get; set; } = null!;


        public ICollection<EXAM_QUETION>? ExamQuestions { get; set; } = null!;

    }
}
