using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alura.ListaLeitura.App.Negocio
{
    public class ReadList
    {
        private List<Book> _livros;

        public ReadList(string titulo, params Book[] livros)
        {
            Titulo = titulo;
            _livros = livros.ToList();
            _livros.ForEach(l => l.Lista = this);
        }

        public string Titulo { get; set; }
        public IEnumerable<Book> Books
        {
            get { return _livros; }
        }

        public override string ToString()
        {
            var linhas = new StringBuilder();
            linhas.AppendLine(Titulo);
            linhas.AppendLine("=========");
            foreach (var livro in Books)
            {
                linhas.AppendLine(livro.ToString());
            }
            return linhas.ToString();
        }
    }
}
