using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Development.Models
{
    /// <summary>
    ///     Model used for storing products in the database
    /// </summary>
    public class Product
    {
        [Key] public int Id { get; set; }
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public double Price { get; set; }
        public bool ForSale { get; set; }
        public int PlateCondition { get; set; }
        public int SleeveCondition { get; set; }
        [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; }

        [ForeignKey("RecordId")] public virtual Record Record { get; set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }
    }
}