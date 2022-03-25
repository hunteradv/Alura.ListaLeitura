using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(Routing);
        }

        public Task Routing (HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            var servedPaths = new Dictionary<string, RequestDelegate>
            {
                { "/Livros/ParaLer", BooksToRead },
                { "/Livros/Lendo", BooksReading },
                { "/Livros/Lidos", BooksAlreadyRead }
            };
            
            if (servedPaths.ContainsKey(context.Request.Path))
            {
                var method = servedPaths[context.Request.Path];
                return method.Invoke(context);
            }

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("404 - Caminho inexistente");
        }

        public Task BooksToRead(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            return context.Response.WriteAsync(repo.ToRead.ToString());            
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