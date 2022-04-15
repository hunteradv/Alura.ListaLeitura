using Alura.ListaLeitura.App.Business;
using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logic
{
    public class BooksController : Controller
    {
        public IEnumerable<Book> Books { get; set; }
        public string Details(int id)
        {            
            var repo = new BookRepositoryCSV();
            var book = repo.All.First(l => l.Id == id);
            return book.Details();
        }

        private static string LoadList(IEnumerable<Book> books)
        {
            var fileContent = HtmlUtils.LoadFileHTML("list");            
            return fileContent.Replace("#NEW-ITEM#", "");
        }

        public IActionResult ToRead()
        {
            var repo = new BookRepositoryCSV();
            ViewBag.Books = repo.ToRead.Books;            
            return View("list");
        }

        public IActionResult Reading()
        {
            var repo = new BookRepositoryCSV();
            ViewBag.Books = repo.Reading.Books;
            return View("list");
        }

        public IActionResult AlreadyRead()
        {
            var repo = new BookRepositoryCSV();
            ViewBag.Books = repo.AlreadyRead.Books;
            return View("list");
        }
    }
}
