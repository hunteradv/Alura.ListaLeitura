using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Mvc
{
    public class DefaultRouting
    {
        public static Task DefaultTreatment(HttpContext context)
        {
            //rota padrão: /<Classe>Logica/Metodo
            //{classe}/{metodo}

            var className = Convert.ToString(context.GetRouteValue("class"));
            var methodName = Convert.ToString(context.GetRouteValue("method"));

            var completeName = $"Alura.ListaLeitura.App.Logic.{className}.Logic";

            var type = Type.GetType(className);
            var method = type.GetMethods().Where(m => m.Name == methodName).First();
            var requestDelegate = (RequestDelegate)Delegate.CreateDelegate(typeof(RequestDelegate), method);

            return requestDelegate.Invoke(context);
        }
    }
}
