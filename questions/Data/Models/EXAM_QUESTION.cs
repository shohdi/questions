using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace questions.Data.Models
{
    [Table("EXAMS_QUETIONS")]


    public class EXAM_QUETION
    {
        [Key]
        [Required]
        [Column(TypeName = "decimal(10)")]
        public long? ID { get; set; }





        [Required]
        [ForeignKey(nameof(EXAM))]
        [Column(TypeName = "decimal(10)")]
        public long? EXAM_ID { get; set; }
        [Required]
        [ForeignKey(nameof(QUESTION))]
        [Column(TypeName = "decimal(10)")]
        public long? QUESTION_ID { get; set; }


    }
}

