using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace questions.Data.Models
{
    [Table("SELECTIONS")]
    [Index(nameof(SELECTION_TEXT))]
    [Index(nameof(IS_ANSWER))]

    public class SELECTION
    {
        [Key]
        [Required]
        [Column(TypeName = "decimal(10)")]
        public long? ID { get; set; }

        [Required]
        public string SELECTION_TEXT { get; set; }

        [Required]
        public bool? IS_ANSWER { get; set; }       


        [Required]
        [ForeignKey(nameof(QUESTION))]
        [Column(TypeName = "decimal(10)")]
        public long? QUESTION_ID { get; set; }


        public QUESTION? Question { get; set; } = null!;


    }
}
