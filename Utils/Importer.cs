using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Web_Development.Models;

namespace Web_Development.Utils
{
    /// <summary>
    ///     Importer class used for importing csv files
    /// </summary>
    public static class Importer
    {
        /// <summary>
        ///     Regular expression used for matching records
        /// </summary>
        // TODO: Improve Regular expression
        private const string Expression = @"^(.*);(.*);(.*);(.*);(.*);.*;(\d{4});.*;.*\((.*)\);.*\((.*)\);.*$";

        /// <summary>
        ///     Regex instance to use for matching
        /// </summary>
        private static readonly Regex Regex = new(Expression, RegexOptions.IgnoreCase);

        /// <summary>
        ///     Creates records that are in the specified contents but not in the specified records
        /// </summary>
        /// <param name="contents">The contents to parse records from</param>
        /// <param name="records">The records to search in, will contain missing records after running</param>
        /// <returns>The records that were added to the specified records list</returns>
        public static List<Record> CreateMissing(string contents, ref List<Record> records)
        {
            // Create list used for storing the created records
            var returned = new List<Record>();

            foreach (var line in contents.Split("\n"))
            {
                // Check if line matches regex
                var match = Regex.Match(line);
                if (!match.Success) continue;

                // Parse groups
                var catalogNumber = match.Groups[1].Value;
                var plate = Models.Extensions.Parse(match.Groups[7].Value);
                var sleeve = Models.Extensions.Parse(match.Groups[8].Value);
                var parseYear = short.TryParse(match.Groups[6].Value, out var year);

                // Check if data is valid and the record doesn't exist
                if (plate == null || sleeve == null || !parseYear ||
                    records.Find(record => record.CatalogNumber == catalogNumber) != null) continue;

                // Create and add a record to list
                // TODO: Get youtube video url from youtube api without spamming requests
                returned.Add(new Record
                {
                    CreatedAt = DateTime.Now, Artist = match.Groups[2].Value, Title = match.Groups[3].Value,
                    Year = year, CatalogNumber = catalogNumber
                });
            }

            // Add created list to existing list and return
            records.AddRange(returned);
            return returned;
        }

        /// <summary>
        ///     Gets a list of products from a csv file's contents
        /// </summary>
        /// <param name="user">The user that is importing the products</param>
        /// <param name="contents">The contents of the csv file</param>
        /// <param name="records">The list of all records currently in the database</param>
        /// <returns>The list of successfully parsed products</returns>
        public static List<Product> GetProducts(User user, string contents, List<Record> records)
        {
            // Initialize lists for output
            var products = new List<Product>();

            foreach (var line in contents.Split("\n"))
            {
                // Check if line matches regex
                var match = Regex.Match(line);
                if (!match.Success) continue;

                // Parse groups
                var plate = Models.Extensions.Parse(match.Groups[7].Value);
                var sleeve = Models.Extensions.Parse(match.Groups[8].Value);
                var parseYear = short.TryParse(match.Groups[6].Value, out var year);
                if (plate == null || sleeve == null || !parseYear) continue;

                // Find already existing record, or create and add to records list
                var found = records.Find(record => record.CatalogNumber == match.Groups[1].Value);

                // Create product from parsed data
                products.Add(new Product
                {
                    ForSale = false,
                    UserId = user.Id,
                    RecordId = found!.Id,
                    CreatedAt = DateTime.Now,
                    Format = match.Groups[6].Value,
                    Label = match.Groups[5].Value,
                    PlateCondition = (Condition) plate,
                    SleeveCondition = (Condition) sleeve
                });
            }

            return products;
        }
    }
}