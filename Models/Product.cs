using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Development.Models
{
    public class Product
    {
        [Key] public long Id { get; set; }

        [Required] public long RecordId { get; set; }

        [Required] public long UserId { get; set; }

        [Required] public double Price { get; set; }

        [Required] public bool ForSale { get; set; }

        [Required] public Condition PlateCondition { get; set; }

        [Required] public Condition SleeveCondition { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("RecordId")] public virtual Record Record { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; }
    }

    public enum Condition
    {
        Fair,
        Good,
        VeryGood,
        VeryGoodPlus,
        NearMint,
        Mint
    }

    public static class Extensions
    {
        /// <summary>
        ///     Gets the abbreviation used for describing a condition
        /// </summary>
        /// <param name="condition">The condition to get the abbreviation for</param>
        /// <returns>The abbreviation, or null if not found</returns>
        public static string GetAbbreviation(this Condition condition)
        {
            return condition switch
            {
                Condition.Fair => "F",
                Condition.Good => "G",
                Condition.VeryGood => "VG",
                Condition.VeryGoodPlus => "VG+",
                Condition.NearMint => "NM or M-",
                Condition.Mint => "M",
                _ => null
            };
        }

        /// <summary>
        ///     Gets the name used for displaying a condition
        /// </summary>
        /// <param name="condition">The condition to get the name for</param>
        /// <returns>The name, or null if not found</returns>
        private static string GetName(this Condition condition)
        {
            return condition switch
            {
                Condition.VeryGood => "Very good",
                Condition.VeryGoodPlus => "Very good+",
                Condition.NearMint => "Near mint",
                _ => condition.ToString()
            };
        }

        /// <summary>
        ///     Gets the text displayed when showing a condition
        /// </summary>
        /// <param name="condition">The condition to get the displayed text for</param>
        /// <returns>The text to display</returns>
        public static string GetDisplayName(this Condition condition)
        {
            return $"{GetName(condition)} ({GetAbbreviation(condition)})";
        }
    }
}