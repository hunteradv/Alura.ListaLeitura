using Alura.ListaLeitura.App.Business;
using System.Collections.Generic;

namespace Alura.ListaLeitura.App.Repository
{
    interface IBookRepository
    {
        ReadList ToRead { get; }
        ReadList Reading { get; }
        ReadList AlreadyRead { get; }
        IEnumerable<Book> All { get; }
        void Include(Book livro);
    }
}
