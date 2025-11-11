using System.Text;
using BookstoreApplication.Models;

namespace SqlInsertGenerator
{
    class Program
    {
        static Random random = new Random();

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
            File.WriteAllText(filePath, string.Empty); // clear file
            var sb = new StringBuilder();

            for (int i = 1; i <= count; i++)
            {
                var author = new Author
                {
                    Id = i,
                    FullName = $"Author {i}",
                    Biography = $"Biography of author {i}",
                    DateOfBirth = RandomDate(new DateTime(1950, 1, 1), new DateTime(2000, 12, 31))
                };

                sb.AppendLine(ToSql(author));

                if (i % 10000 == 0)
                {
                    File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
                    sb.Clear();
                    Console.WriteLine($"Written {i} authors...");
                }
            }

            File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        static void GenerateBooks(string filePath, int count, int maxAuthorId)
        {
            if (maxAuthorId < 1)
                throw new ArgumentException("maxAuthorId must be at least 1. Make sure authors are generated first.");

            File.WriteAllText(filePath, string.Empty); // clear file
            var sb = new StringBuilder();

            for (int i = 1; i <= count; i++)
            {
                var book = new Book
                {
                    Id = i,
                    Title = $"Book {i}",
                    PageCount = random.Next(50, 1001),
                    PublishedDate = RandomDate(new DateTime(1990, 1, 1), DateTime.Today),
                    ISBN = $"{random.Next(100000000, 999999999)}",
                    AuthorId = random.Next(1, maxAuthorId + 1),
                    PublisherId = random.Next(1, 4), // ✅ only 1–3
                    AverageRating = Math.Round(random.NextDouble() * 5, 2)
                };

                sb.AppendLine(ToSql(book));

                if (i % 10000 == 0)
                {
                    File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
                    sb.Clear();
                    Console.WriteLine($"Written {i} books...");
                }
            }

            File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
        // --- Helpers ---
        static DateTime RandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(random.Next(range));
        }

        static string EscapeSql(string s) => s.Replace("'", "''");

        static string ToSql(Author a) =>
        $"INSERT INTO \"Authors\" (\"Id\", \"FullName\", \"Biography\", \"Birthday\") " +
        $"VALUES ({a.Id}, '{EscapeSql(a.FullName)}', '{EscapeSql(a.Biography)}', '{a.DateOfBirth:yyyy-MM-dd}');";

        static string ToSql(Book b) =>
            $"INSERT INTO \"Books\" (\"Id\", \"Title\", \"PageCount\", \"PublishedDate\", \"ISBN\", \"AuthorId\", \"PublisherId\", \"AverageRating\") " +
            $"VALUES ({b.Id}, '{EscapeSql(b.Title)}', {b.PageCount}, '{b.PublishedDate:yyyy-MM-dd}', '{b.ISBN}', {b.AuthorId}, {b.PublisherId}, {b.AverageRating});";
    }
}