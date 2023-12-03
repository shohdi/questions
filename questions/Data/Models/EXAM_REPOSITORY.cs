using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace questions.Data.Models
{
	[Table("EXAM_REPOSITORIES")]
	[Index(nameof(NAME))]
	public class EXAM_REPOSITORY
	{
		[Key]
		[Required]
        [Column(TypeName = "decimal(10)")]
        public long? ID { get; set; }

		[Required]
		public string NAME { get; set; }





	}
}
