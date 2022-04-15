using System.IO;

namespace Alura.ListaLeitura.App.HTML
{
    public class HtmlUtils
    {
        public static string LoadFileHTML(string fileName)
        {
            var fileCompleteName = $"HTML/{fileName}.html";
            using (var file = File.OpenText(fileCompleteName))
            {
                return file.ReadToEnd();
            }
        }
    }
}
