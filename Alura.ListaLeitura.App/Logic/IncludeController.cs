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
        public string Insert(Book book)
        {            
            var repo = new BookRepositoryCSV();
            repo.Include(book);
            return "O livro foi incluido com sucesso!";
        }

        public static Task ShowForm(HttpContext context)
        {
            var html = HtmlUtils.LoadFileHTML("form");
            return context.Response.WriteAsync(html);
        }        
    }
}
