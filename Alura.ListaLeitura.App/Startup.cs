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
            var servedPaths = new Dictionary<string, string>
            {
                { "/Livros/ParaLer", repo.ToRead.ToString() },
                { "/Livros/Lendo", repo.Reading.ToString() },
                { "/Livros/Lidos", repo.AlreadyRead.ToString() }
            };
            
            if (servedPaths.ContainsKey(context.Request.Path))
            {
                return context.Response.WriteAsync(servedPaths[context.Request.Path]);
            }

            return context.Response.WriteAsync("404 - Caminho inexistente");
        }

        public Task BooksToRead(HttpContext context)
        {
            var repo = new BookRepositoryCSV();
            return context.Response.WriteAsync(repo.ToRead.ToString());            
        }
    }
}