using System.Text;

namespace Alura.ListaLeitura.App.Business
{
    public class Book
    {
        public int Id { get; set; }    
        public string Title { get; set; }
        public string Author { get; set; }
        public ReadList List { get; set; }

        public string Details()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Detalhes do Livro");
            stringBuilder.AppendLine("=====");
            stringBuilder.AppendLine($"Título: {Title}");
            stringBuilder.AppendLine($"Autor: {Author}");
            stringBuilder.AppendLine($"Lista: {List.Title}");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
