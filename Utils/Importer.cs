using System.Collections.Generic;
using System.Text.RegularExpressions;
using Web_Development.Models;

namespace Web_Development.Utils
{
    public static class Importer
    {
        /// <summary>
        ///     Regular expression used for matching records
        /// </summary>
        private const string Expression = @"^(.*);(.*);(.*);(.*);(.*);.*;(\d{4});.*;.*\((.*)\);.*\((.*)\);.*$";

        /// <summary>
        ///     Regex instance to use for matching
        /// </summary>
        private static readonly Regex Regex = new(Expression, RegexOptions.IgnoreCase);

        /// <summary>
        ///     Gets a list of products from a csv file's contents
        /// </summary>
        /// <param name="user">The user that is importing the products</param>
        /// <param name="contents">The contents of the csv file</param>
        /// <param name="records">The list of all records after parsing</param>
        /// <returns>The list of successfully parsed products</returns>
        public static List<Product> GetProducts(User user, string contents, ref List<Record> records)
        {
            // Initialize lists for 
            var products = new List<Product>();

            foreach (var line in contents.Split("\n"))
            {
                // Check if line matches regex
                var match = Regex.Match(line);
                if (!match.Success) continue;

                // Parse groups
                var plate = Extensions.Parse(match.Groups[7].Value);
                var sleeve = Extensions.Parse(match.Groups[8].Value);
                var parseYear = short.TryParse(match.Groups[6].Value, out var year);
                if (plate == null || sleeve == null || !parseYear) continue;

                // Find already existing record, or create and add to records list
                var record = FindOrCreate(match.Groups[2].Value, match.Groups[3].Value, year, match.Groups[1].Value,
                    ref records);

                // Create product from parsed data
                products.Add(new Product
                {
                    User = user, Record = record, ForSale = false, PlateCondition = (Condition) plate,
                    SleeveCondition = (Condition) sleeve
                });
            }

            // Return successfully parsed records
            return products;
        }

        /// <summary>
        ///     Finds a record in the specified list, or creates it if it doesn't exist
        /// </summary>
        /// <param name="artist">The artist of the record</param>
        /// <param name="title">The title of the record</param>
        /// <param name="year">The year the record was created</param>
        /// <param name="catalogNumber">The catalog number of the record</param>
        /// <param name="records">The list of records that already exist</param>
        /// <returns>The record that was found/created</returns>
        private static Record FindOrCreate(string artist, string title, short year, string catalogNumber,
            ref List<Record> records)
        {
            // Find record and return if found
            var found = records.Find(record => record.CatalogNumber == catalogNumber);
            if (found != null) return found;

            // Create new record for the specified data
            // TODO: Get youtube video url from youtube api without spamming requests 
            var created = new Record {Artist = artist, Title = title, Year = year, CatalogNumber = catalogNumber};
            records.Add(created);
            return created;
        }
    }
}