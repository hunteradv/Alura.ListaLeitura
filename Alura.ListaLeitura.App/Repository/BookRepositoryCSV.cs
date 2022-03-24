using System;
using System.Collections.Generic;
using System.Text;
using Alura.ListaLeitura.App.Negocio;
using System.IO;
using System.Linq;

namespace Alura.ListaLeitura.App.Repositorio
{
    public class BookRepositoryCSV : IBookRepository
    {
        private static readonly string fileNameCSV = "Repository\\Books.csv";

        private ReadList _toRead;
        private ReadList _reading;
        private ReadList _alreadyRead;

        public BookRepositoryCSV()
        {
            var arrayToRead = new List<Book>();
            var arrayReading = new List<Book>();
            var arrayAlreadyRead = new List<Book>();

            using (var file = File.OpenText(BookRepositoryCSV.fileNameCSV))
            {
                while (!file.EndOfStream)
                {
                    var bookText = file.ReadLine();
                    if (string.IsNullOrEmpty(bookText))
                    {
                        continue;
                    }
                    var bookInfo = bookText.Split(';');
                    var book = new Book
                    {
                        Id = Convert.ToInt32(bookInfo[1]),
                        Title = bookInfo[2],
                        Author = bookInfo[3]
                    };
                    switch (bookInfo[0])
                    {
                        case "para-ler":
                            arrayToRead.Add(book);
                            break;
                        case "lendo":
                            arrayReading.Add(book);
                            break;
                        case "lidos":
                            arrayAlreadyRead.Add(book);
                            break;
                        default:
                            break;
                    }
                }
            }

            _toRead = new ReadList("Para Ler", arrayToRead.ToArray());
            _reading = new ReadList("Lendo", arrayReading.ToArray());
            _alreadyRead = new ReadList("Lidos", arrayAlreadyRead.ToArray());
        }

        public ReadList ToRead => _toRead;
        public ReadList Reading => _reading;
        public ReadList AlreadyRead => _alreadyRead;

        public IEnumerable<Book> All => _toRead.Books.Union(_reading.Books).Union(_alreadyRead.Books);

        public void Include(Book book)
        {
            var id = All.Select(l => l.Id).Max();
            using (var file = File.AppendText(BookRepositoryCSV.fileNameCSV))
            {
                file.WriteLine($"para-ler;{id+1};{book.Title};{book.Author}");
            }
        }
    }
}
