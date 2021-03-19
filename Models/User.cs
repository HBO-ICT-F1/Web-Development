using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Development.Models
{
    /// <summary>
    ///     Model used for storing users in the database
    /// </summary>
    public class User
    {
        [Key] public int Id { get; set; }
        [DataType(DataType.Text)] public string Name { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
        public short Role { get; set; }
        [DataType(DataType.Password)] public string Password { get; set; }
        [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PostalCode)] public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}