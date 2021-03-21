using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Development.Models
{
    /// <summary>
    ///     Model used for storing records in the database
    /// </summary>
    public class Record
    {
        /// <summary>
        ///     Unique record id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///     Record artist name
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Artist { get; set; }

        /// <summary>
        ///     Record title
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        ///     Record youtube token for displaying a preview of the song
        /// </summary>
        [StringLength(255)]
        public string YoutubeToken { get; set; }

        /// <summary>
        ///     Record year at which it was released
        /// </summary>
        [Required]
        public short Year { get; set; }

        /// <summary>
        ///     Record catalog number
        /// </summary>
        public int CatalogNumber { get; set; }

        /// <summary>
        ///     The time at which this record was uploaded
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}