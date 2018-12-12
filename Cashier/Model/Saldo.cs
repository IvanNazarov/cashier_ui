using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cashier.Model
{
    [Table("saldo")]
    public class Saldo
    {
        //Измерения
        [Required]
        public int Id { get; set; }
        public Product Product { get; set; }

        [Required]
        [Index]
        public int ProductId { get; set; }

        public DateTime OperationDate { get; set; }

        //документа у нас пока нет, но будет.... я надеюсь...
        [Index]
        public int DocumentId { get; set; }

        //Ресурсы
        public double Debt { get; set; }
        public double Credt { get; set; }
    }
}
