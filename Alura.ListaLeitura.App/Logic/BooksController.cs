using Alura.ListaLeitura.App.Business;
using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logic
{
    public class BooksController
    {
        public string Details(int id)
        {            
            var repo = new BookRepositoryCSV();
            var book = repo.All.First(l => l.Id == id);
            return book.Details();
        }

        private static string LoadList(IEnumerable<Book> books)
        {
            var fileContent = HtmlUtils.LoadFileHTML("to-read");

            foreach (var book in books)
            {
                fileContent = fileContent.Replace("#NEW-ITEM#", $"<li>{book.Title} - {book.Author}</li>#NEW-ITEM#");
            }
            return fileContent.Replace("#NEW-ITEM#", "");
        }

        public static Task ToRead(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            var html = LoadList(repo.ToRead.Books);

            return context.Response.WriteAsync(html);
        }

        public static Task Reading(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            return context.Response.WriteAsync(repo.Reading.ToString());
        }

        public static Task AlreadyRead(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            return context.Response.WriteAsync(repo.AlreadyRead.ToString());
        }

        public string Test()
        {
            return "Nova funcionalidade foi implementada!";
        }
    }
}
