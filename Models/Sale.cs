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
        /// <summary>
        ///     Unique sale id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///     The unique id of the product that was sold
        /// </summary>
        [Required]
        public long ProductId { get; set; }

        /// <summary>
        ///     The unique id of the user that ordered the product
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        ///     The time at which the order was placed
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     The product instance of the ProductId field
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        /// <summary>
        ///     The user instance of the UserId field
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}