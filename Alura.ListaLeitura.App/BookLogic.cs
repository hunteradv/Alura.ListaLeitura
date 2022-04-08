using Alura.ListaLeitura.App.Business;
using Alura.ListaLeitura.App.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class BookLogic
    {
        private Task ShowDetails(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new BookRepositoryCSV();
            var book = repo.All.First(l => l.Id == id);
            return context.Response.WriteAsync(book.Details());
        }

        private string LoadFileHTML(string fileName)
        {
            var fileCompleteName = $"HTML/{fileName}.html";
            using (var file = File.OpenText(fileCompleteName))
            {
                return file.ReadToEnd();
            }
        }

        private string LoadList(IEnumerable<Book> books)
        {
            var fileContent = LoadFileHTML("to-read");

            foreach (var book in books)
            {
                fileContent = fileContent.Replace("#NEW-ITEM#", $"<li>{book.Title} - {book.Author}</li>#NEW-ITEM#");
            }
            return fileContent.Replace("#NEW-ITEM#", "");
        }

        public Task BooksToRead(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            var html = LoadList(repo.ToRead.Books);

            return context.Response.WriteAsync(html);
        }

        public Task BooksReading(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            return context.Response.WriteAsync(repo.Reading.ToString());
        }

        public Task BooksAlreadyRead(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            return context.Response.WriteAsync(repo.AlreadyRead.ToString());
        }
    }
}
