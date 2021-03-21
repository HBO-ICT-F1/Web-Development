using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Development.Models
{
    /// <summary>
    ///     Model used for storing users in the database
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Unique user id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///     Username
        /// </summary>
        [DataType(DataType.Text)]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     User email address
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///     User role
        ///     0 is used for normal users.
        ///     1 is used for sellers.
        /// </summary>
        [Required]
        public short Role { get; set; }

        /// <summary>
        ///     User password hashed with BCrypt
        /// </summary>
        [StringLength(255)] // TODO: Actual BCrypt hash length
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     The time at which this user registered
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     User address, used for delivering the record after purchase
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        ///     User postalcode, used for delivering the record after purchase
        /// </summary>
        [DataType(DataType.PostalCode)]
        [Required]
        public string PostalCode { get; set; }

        /// <summary>
        ///     User country, used for delivering the record after purchase
        /// </summary>
        [Required]
        public string Country { get; set; }
    }
}