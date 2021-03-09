namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            Console.WriteLine(RemoveBooks(db));
        }

        public static int RemoveBooks(BookShopContext context)
        {

            var booksToDel = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var result = booksToDel.Count();

            context.Books.RemoveRange(booksToDel);

            context.SaveChanges();

            return result;
        }


        public static void IncreasePrices(BookShopContext context)
        {

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }


        public static string GetMostRecentBooks(BookShopContext context)
        {

            var categoryBooks = context.Categories
                .Select(x => new
                {
                    CategoryName = x.Name,
                    Books = x.CategoryBooks.Select(b => new
                    {
                        Title = b.Book.Title,
                        ReleaseDate = b.Book.ReleaseDate.Value
                    })
                    .OrderByDescending(x => x.ReleaseDate)
                    .Take(3)
                    .ToList()

                })
                .OrderBy(x => x.CategoryName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var cat in categoryBooks)
            {
                sb.AppendLine($"--{cat.CategoryName}");

                foreach (var book in cat.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Year})");
                }
            }       

            return sb.ToString().TrimEnd();
        }


        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    profit = x.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                })
                .OrderByDescending(p => p.profit)
                .ThenBy(c => c.Name)
                .ToList();

            var sb = new StringBuilder();

            foreach (var cat in categories)
            {
                sb.AppendLine($"{cat.Name} ${cat.profit:f2}");
            }

            return sb.ToString().TrimEnd();
        }


        public static string CountCopiesByAuthor(BookShopContext context)
        {

            var autors = context.Authors
                .Select(x => new
                {
                   x.FirstName,
                   x.LastName,
                   booksCount = x.Books.Select(c => c.Copies).Sum()

                })
                .OrderByDescending(x => x.booksCount)
                .ToList();

            var sb = new StringBuilder();

            foreach (var autor in autors)
            {
                sb.AppendLine($"{autor.FirstName} {autor.LastName} - {autor.booksCount}");
            }

            return sb.ToString().TrimEnd();
        }


        public static int CountBooks(BookShopContext context, int lengthCheck) 
        {

            var books = context.Books
                .Where(book => book.Title.Length > lengthCheck)
                .ToList();

            return books.Count();
        
        }

        public static string GetBooksByAuthor(BookShopContext context, string input) 
        {
            var books = context.Books
                .Where(book => book.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(x => new
                {
                    x.BookId,
                    x.Title,
                    x.Author.FirstName,
                    x.Author.LastName

                })
                .OrderBy(x => x.BookId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return sb.ToString().TrimEnd();
        }


        public static string GetBookTitlesContaining(BookShopContext context, string input) 
        {

            var books = context.Books
                .Where(book => book.Title.ToLower().Contains(input.ToLower()))
                .Select(x => new { x.Title })
                .OrderBy(x => x.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, books.Select(x => x.Title));
            return result;
        }


        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {

            var autors = context.Authors
                .Where(autor => autor.FirstName.EndsWith(input))
                .Select(autor => new
                {
                    autor.FirstName,
                    autor.LastName
                })
                .OrderBy(autor => autor.FirstName)
                .ThenBy(autor => autor.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var autor in autors)
            {
                sb.AppendLine($"{autor.FirstName} {autor.LastName}");
            }

            return sb.ToString().TrimEnd();
        }


        public static string GetBooksReleasedBefore(BookShopContext context, string date) 
        {

            var targetDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                    .Where(book => book.ReleaseDate.Value < targetDate)
                    .Select(book => new
                    {
                        book.Title,
                        book.EditionType,
                        book.Price,
                        book.ReleaseDate
                    })
                    .OrderByDescending(book => book.ReleaseDate)
                    .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();

        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {

            var categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToList();

            var books = context.Books
                .Include(x => x.BookCategories)
                .ThenInclude(x => x.Category)
                .ToList()
                .Where(book => book.BookCategories
                        .Any(category => categories.Contains(category.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(title => title)
                .ToList();
           
            var result = string.Join(Environment.NewLine, books);

            return result;

        }


        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {

            var books = context.Books
                .Where(book => book.ReleaseDate.Value.Year != year)
                .Select(x => new
                {
                    x.Title,
                    x.BookId
                })
                .OrderBy(x => x.BookId)
                .ToList();

            var result = string.Join(Environment.NewLine, books.Select(x => x.Title));

            return result;

        }



        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(book => book.Price > 40)
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .OrderByDescending(x => x.Price)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }


        public static string GetGoldenBooks(BookShopContext context)
        {
            var allBooks = context.Books
                .Where(books => books.EditionType == EditionType.Gold
                        && books.Copies < 5000)
                .Select(x => new
                {
                    x.Title,
                    x.BookId
                })
                .OrderBy(x => x.BookId)
                .ToList();

            var result = string.Join(Environment.NewLine, allBooks.Select(x =>x.Title));
            return result;

        }


        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(books => books.AgeRestriction == ageRestriction)
                .Select(books => books.Title)
                .OrderBy(title => title)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }



    }
}
