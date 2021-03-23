using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Development.Models
{
    public class Record
    {
        [Key] public long Id { get; set; }

        [StringLength(255)] [Required] public string Artist { get; set; }

        [StringLength(255)] [Required] public string Title { get; set; }

        [StringLength(255)] public string YoutubeToken { get; set; }

        [Required] public short Year { get; set; }

        public int CatalogNumber { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}