using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace questions.Data.Models
{
    [Table("QUESTIONS")]
    [Index(nameof(IMAGE_PATH))]
    [Index(nameof(QUESTION_TEXT))]
    public class QUESTION
    {
        [Key]
        [Required]
        [Column(TypeName = "decimal(10)")]
        public long? ID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(2000)")]
        public string QUESTION_TEXT { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string IMAGE_PATH { get; set; }


        [Required]
        [ForeignKey(nameof(EXAM_REPOSITORY))]
        [Column(TypeName = "decimal(10)")]
        public long? REPO_ID { get; set; }


        public EXAM_REPOSITORY? ExamRepository { get; set; } = null!;

        public ICollection<SELECTION>? Selections { get; set; } = null!;


    }
}