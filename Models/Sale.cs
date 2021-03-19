using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Development.Models
{
    /// <summary>
    ///     Model used for storing sales in the database
    /// </summary>
    public class Sale
    {
        [Key] public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; }

        [ForeignKey("ProductId")] public virtual Product Product { get; set; }
        [ForeignKey("UserId")] public virtual User User { get; set; }
    }
}