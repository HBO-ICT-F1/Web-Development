using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Development.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        
        [DataType(DataType.Text)]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        
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
        
        [StringLength(60)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [DataType(DataType.PostalCode)]
        [Required]
        public string PostalCode { get; set; }
        
        [Required]
        public string Country { get; set; }
    }
}