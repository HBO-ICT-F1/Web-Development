using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Development.Models
{
    /// <summary>
    ///     Model used for storing records in the database
    /// </summary>
    public class Record
    {
        [Key] public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string YoutubeToken { get; set; }
        public int Year { get; set; }
        public int CatalogNumber { get; set; }
        [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; }
    }
}