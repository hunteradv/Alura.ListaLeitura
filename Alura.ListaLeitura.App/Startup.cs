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
            builder.MapRoute("Livros/ParaLer", BooksLogic.ToRead);
            builder.MapRoute("Livros/Lendo", BooksLogic.Reading);
            builder.MapRoute("Livros/Lidos", BooksLogic.AlreadyRead);
            builder.MapRoute("Cadastro/NovoLivro/{name}/{author}", IncludeLogic.NewBook);
            builder.MapRoute("Livros/Detalhes/{id:int}", BooksLogic.Details);
            builder.MapRoute("Cadastro/ExibeFormulario", IncludeLogic.ShowForm);
            builder.MapRoute("Cadastro/Incluir", IncludeLogic.Insert);
            var routes = builder.Build();

            app.UseRouter(routes);

            //app.Run(Routing);
        }          
    }
}