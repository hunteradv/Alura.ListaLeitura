using Alura.ListaLeitura.App.Business;
using Alura.ListaLeitura.App.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ShowForm()
        {
            //var html = HtmlUtils.LoadFileHTML("form");
            var html = new ViewResult { ViewName = "form" };
            return html;
        }      
    }
}
