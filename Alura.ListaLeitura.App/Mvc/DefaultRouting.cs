using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Mvc
{
    public class DefaultRouting
    {
        public static Task DefaultTreatment(HttpContext context)
        {
            //rota padrão: /<Classe>Logica/Metodo
            //{classe}/{metodo}

            var className = Convert.ToString(context.GetRouteValue("className"));
            var methodName = Convert.ToString(context.GetRouteValue("methodName"));

            var completeName = $"Alura.ListaLeitura.App.Logic.{className}Logic";

            var type = Type.GetType(completeName);
            var method = type.GetMethods().Where(m => m.Name == methodName).FirstOrDefault();
            var requestDelegate = (RequestDelegate)Delegate.CreateDelegate(typeof(RequestDelegate), method);

            return requestDelegate.Invoke(context);
        }
    }
}
