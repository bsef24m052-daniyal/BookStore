using HW_5.Models;
using HW_5.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_5.Controllers
{
    public class HomeController : Controller
    {
        public List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "James Smith", Country = "USA" },
            new Author { Id = 2, Name = "Shoya Nakamura", Country = "JAPAN" },
        };
        public static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Price = 10.99m, AuthorId = 1 },
            new Book { Id = 2, Title = "Book 2", Price = 15.99m, AuthorId = 2 },
            new Book { Id = 3, Title = "Book 3", Price = 12.99m, AuthorId = 1 },
        };
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Books = Books.Select(b => new BookCardViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    AuthorName = Authors.FirstOrDefault(a => a.Id == b.AuthorId)?.Name
                }).ToList(),
                Authors = Authors
            };
            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            var newBook = new Book
            {
                Id = Books.Count + 1,
                Title = title,
                Price = price,
                AuthorId = authorId
            };
            Books.Add(newBook);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            var newAuthor = new Author
            {
                Id = Authors.Count + 1,
                Name = name,
                Country = country
            };
            Authors.Add(newAuthor);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            var bookToDelete = Books.FirstOrDefault(b => b.Id == id);
            if (bookToDelete != null)
            {
                Books.Remove(bookToDelete);
            }
            return RedirectToAction("Index");
        }


    }
}
