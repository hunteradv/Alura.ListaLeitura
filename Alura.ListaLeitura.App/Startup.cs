using Alura.ListaLeitura.App.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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
            builder.MapRoute("Livros/ParaLer", BooksLogic.BooksToRead);
            builder.MapRoute("Livros/Lendo", BooksLogic.BooksReading);
            builder.MapRoute("Livros/Lidos", BooksLogic.BooksAlreadyRead);
            builder.MapRoute("Cadastro/NovoLivro/{name}/{author}", IncludeLogic.NewBookToRead);
            builder.MapRoute("Livros/Detalhes/{id:int}", BooksLogic.ShowDetails);
            builder.MapRoute("Cadastro/NovoLivro", IncludeLogic.ShowForm);
            builder.MapRoute("Cadastro/Incluir", IncludeLogic.ProcessForm);
            var routes = builder.Build();

            app.UseRouter(routes);

            //app.Run(Routing);
        }          
    }
}