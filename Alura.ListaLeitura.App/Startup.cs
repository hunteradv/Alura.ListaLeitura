using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            var routes = builder.Build();

            app.UseRouter(routes);

            //app.Run(Routing);
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