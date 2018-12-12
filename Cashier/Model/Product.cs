using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cashier.Model
{
    [Table("products")]
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Name { get; set; }
        public Measure Measure { get; set; }
        public int MeasureId { get; set; }
        public double Price { get; set; }
        public bool IsWeight { get; set; }
    }
}
