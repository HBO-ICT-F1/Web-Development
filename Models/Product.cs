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
        /// <summary>
        ///     Unique product id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///     The unique id of the record this product is for
        /// </summary>
        [Required]
        public long RecordId { get; set; }

        /// <summary>
        ///     The unique id of the user who is selling this product
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        ///     The price requested for this product
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        ///     Whether this product is for sale
        /// </summary>
        [Required]
        public bool ForSale { get; set; }

        /// <summary>
        ///     The condition of the product
        /// </summary>
        [Required]
        public int PlateCondition { get; set; }

        /// <summary>
        ///     The condition of the products sleeve
        /// </summary>
        [Required]
        public int SleeveCondition { get; set; }

        /// <summary>
        ///     The time at which this product was made available
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     The record instance of the RecordId field
        /// </summary>
        [ForeignKey("RecordId")]
        public virtual Record Record { get; set; }

        /// <summary>
        ///     The user instance of the UserId field
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}