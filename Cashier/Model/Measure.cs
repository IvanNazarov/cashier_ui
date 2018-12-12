using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cashier.Model
{
    [Table("measure")]
    public class Measure
    {
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name",TypeName ="varchar")]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(200)]
        [Column("full_name",TypeName ="varchar")]
        public string FullName { get; set; }
    }
} 