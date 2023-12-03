using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace questions.Data.Models
{
    [Table("ANSWERS")]


    public class ANSWER
    {
        [Key]
        [Required]
        [Column(TypeName = "decimal(10)")]
        public long? ID { get; set; }





        [Required]
        [ForeignKey(nameof(EXAM_QUETION))]
        [Column(TypeName = "decimal(10)")]
        public long? EXAM_QUESTION_ID { get; set; }
        

        [ForeignKey(nameof(SELECTION))]
        [Column(TypeName = "decimal(10)")]
        public long? SELECTION_ID { get; set; }


    }
}

