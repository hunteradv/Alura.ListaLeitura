using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", BooksToRead);
            builder.MapRoute("Livros/Lendo", BooksReading);
            builder.MapRoute("Livros/Lidos", BooksAlreadyRead);
            builder.MapRoute("Cadastro/NovoLivro/{name}/{author}", NewBookToRead);
            builder.MapRoute("Livros/Detalhes/{id:int}", ShowDetails);
            builder.MapRoute("Cadastro/NovoLivro", ShowForm);
            builder.MapRoute("Cadastro/Incluir", ProcessForm);
            var routes = builder.Build();

            app.UseRouter(routes);

            //app.Run(Routing);
        }

        private Task ProcessForm(HttpContext context)
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

        private Task ShowForm(HttpContext context)
        {
            var html = LoadFileHTML("form");
            return context.Response.WriteAsync(html);
        }

        private string LoadFileHTML(string fileName)
        {
            var fileCompleteName = $"HTML/{fileName}.html";
            using (var file = File.OpenText(fileCompleteName))
            {
                return file.ReadToEnd();
            }
        }

        private Task ShowDetails(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new BookRepositoryCSV();
            var book = repo.All.First(l => l.Id == id);
            return context.Response.WriteAsync(book.Details());
        }

        public Task NewBookToRead(HttpContext context)
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