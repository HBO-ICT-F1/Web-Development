using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Development.Models
{
    public class Product
    {
        [Key] public long Id { get; set; }

        [Required] public long RecordId { get; set; }

        [Required] public long UserId { get; set; }

        [Required] public double Price { get; set; }

        [Required] public bool ForSale { get; set; }

        [Required] public int PlateCondition { get; set; }

        [Required] public int SleeveCondition { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("RecordId")] public virtual Record Record { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; }


        public Record getRecord()
        {
            return null;
        }
    }
}