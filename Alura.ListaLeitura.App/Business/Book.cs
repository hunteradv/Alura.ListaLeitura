using System.Text;

namespace Alura.ListaLeitura.App.Negocio
{
    public class Book
    {
        public int Id { get; set; }    
        public string Title { get; set; }
        public string Author { get; set; }
        public ReadList Lista { get; set; }

        public string Detalhes()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Detalhes do Livro");
            stringBuilder.AppendLine("=====");
            stringBuilder.AppendLine($"Título: {Title}");
            stringBuilder.AppendLine($"Autor: {Author}");
            stringBuilder.AppendLine($"Lista: {Lista.Title}");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
