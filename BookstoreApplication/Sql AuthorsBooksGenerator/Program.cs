using System;
using System.IO;
using System.Text;

namespace SqlInsertGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string authorFile = "InsertAuthors.sql";
            string bookFile = "InsertBooks.sql";

            int totalAuthors = 1_000_000;
            int totalBooks = 1_000_000;

            Console.WriteLine("Generating Author inserts...");
            GenerateAuthors(authorFile, totalAuthors);
            Console.WriteLine($"Author inserts saved to {authorFile}");

            Console.WriteLine("Generating Book inserts...");
            GenerateBooks(bookFile, totalBooks, totalAuthors);
            Console.WriteLine($"Book inserts saved to {bookFile}");
        }

        static void GenerateAuthors(string filePath, int count)
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= count; i++)
            {
                string fullName = $"Author {i}";
                string biography = $"Biography of author {i}";
                DateTime dob = RandomDate(new DateTime(1950, 1, 1), new DateTime(2000, 12, 31));

                sb.AppendLine(
                    $"INSERT INTO \"Authors\" (\"Id\", \"FullName\", \"Biography\", \"DateOfBirth\") VALUES ({i}, '{EscapeSql(fullName)}', '{EscapeSql(biography)}', '{dob:yyyy-MM-dd}');"
                );

                if (i % 10000 == 0)
                {
                    File.AppendAllText(filePath, sb.ToString());
                    sb.Clear();
                    Console.WriteLine($"Written {i} authors...");
                }
            }
            File.AppendAllText(filePath, sb.ToString());
        }

        static void GenerateBooks(string filePath, int count, int maxAuthorId)
        {
            if (maxAuthorId < 1)
                throw new ArgumentException("maxAuthorId must be at least 1. Make sure authors are generated first.");

            var sb = new StringBuilder();
            var random = new Random();

            for (int i = 1; i <= count; i++)
            {
                string title = $"Book {i}";
                int pageCount = random.Next(50, 1001); // 50–1000 pages
                DateTime publishedDate = RandomDate(new DateTime(1990, 1, 1), DateTime.Today);
                string isbn = $"{random.Next(100000000, 999999999)}"; // 9-digit ISBN
                int authorId = random.Next(1, maxAuthorId + 1);        // random author
                int publisherId = random.Next(1, 101);                // assuming 100 publishers
                double averageRating = Math.Round(random.NextDouble() * 5, 2);

                sb.AppendLine(
                    $"INSERT INTO \"Books\" (\"Id\", \"Title\", \"PageCount\", \"PublishedDate\", \"ISBN\", \"AuthorId\", \"PublisherId\", \"AverageRating\") " +
                    $"VALUES ({i}, '{EscapeSql(title)}', {pageCount}, '{publishedDate:yyyy-MM-dd}', '{isbn}', {authorId}, {publisherId}, {averageRating});"
                );

                if (i % 10000 == 0)
                {
                    File.AppendAllText(filePath, sb.ToString());
                    sb.Clear();
                    Console.WriteLine($"Written {i} books...");
                }
            }

            File.AppendAllText(filePath, sb.ToString());
        }



        static DateTime RandomDate(DateTime start, DateTime end)
        {
            var random = new Random();
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }

        static string EscapeSql(string s)
        {
            return s.Replace("'", "''"); // escape single quotes
        }
    }
}
