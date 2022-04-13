using Alura.ListaLeitura.App.Business;
using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logic
{
    public class IncludeController
    {      
        public static Task Insert(HttpContext context)
        {
            var book = new Book()
            {
                Title = context.Request.Form["title"].First(),
                Author = context.Request.Form["author"].First(),
            };

            var repo = new BookRepositoryCSV();
            repo.Include(book);
            return context.Response.WriteAsync("O livro foi incluido com sucesso!");
        }

        public static Task ShowForm(HttpContext context)
        {
            var html = HtmlUtils.LoadFileHTML("form");
            return context.Response.WriteAsync(html);
        }

        public static Task NewBook(HttpContext context)
        {
            var book = new Book()
            {
                Title = Convert.ToString(context.GetRouteValue("name")),
                Author = Convert.ToString(context.GetRouteValue("author")),
            };

            var repo = new BookRepositoryCSV();
            repo.Include(book);
            return context.Response.WriteAsync("O livro foi incluido com sucesso!");
        }
    }
}
