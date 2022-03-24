using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repo = new BookRepositoryCSV();

            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();

            //PrintList(_repo.ParaLer);
            //PrintList(_repo.Lendo);
            //PrintList(_repo.Lidos);
        }

        static void PrintList(ReadList list)
        {
            Console.WriteLine(list);
        }
    }
}
