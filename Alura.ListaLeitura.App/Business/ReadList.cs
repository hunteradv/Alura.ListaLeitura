 using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alura.ListaLeitura.App.Negocio
{
    public class ReadList
    {
        private List<Book> _books;

        public ReadList(string title, params Book[] books)
        {
            Title = title;
            _books = books.ToList();
            _books.ForEach(l => l.Lista = this);
        }

        public string Title { get; set; }
        public IEnumerable<Book> Books
        {
            get { return _books; }
        }

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(Title);
            lines.AppendLine("=========");
            foreach (var book in Books)
            {
                lines.AppendLine(book.ToString());
            }
            return lines.ToString();
        }
    }
}
