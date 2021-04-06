using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web_Development.Models
{
    public class Product
    {
        [Key] public long Id { get; set; }

        [Required] public long RecordId { get; set; }

        [Required] public long UserId { get; set; }

        [Required] public double Price { get; set; }

        [Required] public string Format { get; set; }

        [Required] public string Label { get; set; }

        [Required] public bool ForSale { get; set; }

        [Required] public Condition PlateCondition { get; set; }

        [Required] public Condition SleeveCondition { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("RecordId")] public virtual Record Record { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; }

        public virtual Sale Sale { get; set; }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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
        ///     A list of abbreviations for all conditions
        /// </summary>
        private static List<string> Abbreviations { get; } = new() {"F", "G", "VG", "VG+", "NM or M-", "M"};

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
        ///     Parses the specified input to a condition
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <returns>The parsed condition, or null if not found</returns>
        public static Condition? Parse(string input)
        {
            for (var i = 0; i < Abbreviations.Count; i++)
            {
                if (!input.Equals(Abbreviations[i], StringComparison.OrdinalIgnoreCase)) continue;
                return (Condition) i;
            }

            return null;
        }

        /// <summary>
        ///     Gets the text displayed when showing a condition
        /// </summary>
        /// <param name="condition">The condition to get the displayed text for</param>
        /// <returns>The text to display</returns>
        public static string GetDisplayName(this Condition condition)
        {
            return $"{GetName(condition)} ({Abbreviations[(int) condition]})";
        }
    }
}