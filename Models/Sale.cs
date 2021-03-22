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
        [Key]
        public long Id { get; set; }
        
        [Required]
        public long ProductId { get; set; }
        
        [Required]
        public long UserId { get; set; }
        
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}