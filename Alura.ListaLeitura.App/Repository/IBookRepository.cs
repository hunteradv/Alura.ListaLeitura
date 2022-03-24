using Alura.ListaLeitura.App.Negocio;
using System.Collections.Generic;

namespace Alura.ListaLeitura.App.Repositorio
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
