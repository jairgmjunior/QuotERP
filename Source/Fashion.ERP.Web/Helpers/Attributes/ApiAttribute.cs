using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Fashion.ERP.Web.Helpers.ActionResults;
using Fashion.Framework.Common.Extensions;

namespace Fashion.ERP.Web.Helpers.Attributes
{
    /// <summary>
    /// Expôe uma Action como 'JSON/XML API Endpoints', para poder ser usado em uma chamada Ajax sem modificação.
    /// Quando a Action for chamada via Get/Post, será retornada um Html normal para o browser,
    /// mas quando for via Ajax, será retornado o Model serializado como json/xml.
    /// </summary>
    public class ApiAttribute : ActionFilterAttribute
    {
        #region Variáveis
        private readonly static string[] JsonTypes = new[] { "application/json", "text/json" };
        private readonly static string[] XmlTypes = new[] { "application/xml", "text/xml" };
        private readonly static string[] HtmlTypes = { "application/xhtml", "text/html" };
        private const string FormEncodedType = "application/x-www-form-urlencoded";
        private readonly static Regex TypeRegEx = new Regex(@"[\w\*]+/[\w\*]+");
        private readonly static MethodInfo JavascriptDeserialize = typeof(JavaScriptSerializer).GetMethods().Single(m => m.Name == "Deserialize" && m.IsGenericMethod);
        #endregion

        #region CreateInputDictionary
        private static Dictionary<string, string> CreateInputDictionary(ActionExecutingContext filterContext)
        {
            var inputStream = filterContext.HttpContext.Request.InputStream;
            var parameters = filterContext.ActionDescriptor.GetParameters();

            if (inputStream.Length == 0 || parameters.Length == 0)
                return null;

            using (var reader = new StreamReader(inputStream))
            {
                // get the params that are using the [ApiBind] attribute
                var apiBindParameters = parameters
                    .Where(p => p.GetCustomAttributes(typeof(ApiBindAttribute), false).Length > 0)
                    .Select(p => p.ParameterName)
                    .ToArray();

                if (filterContext.HttpContext.Request.ContentType.Contains(FormEncodedType))
                    // the request is form post in key=value format, build the input from that
                    return reader.ReadToEnd()
                        .Split(new[] { '&' })
                        .Select(kvp => kvp.Split(new[] { '=' }))
                        .Where(kvp => !apiBindParameters.Any() || apiBindParameters.Contains(kvp[0], StringComparer.InvariantCultureIgnoreCase))
                        .ToDictionary(kvp => kvp[0], kvp => HttpUtility.UrlDecode(kvp[1]));

                if (parameters.Length > 1 && apiBindParameters.Length != 1)
                    throw new ArgumentOutOfRangeException("", "Unable to infer which parameter should be bound from the request input. Use the [ApiBind] attribute on the parameter that should be bound to the input.");

                return new Dictionary<string, string> { { parameters.Length == 1 ? parameters.Single().ParameterName : apiBindParameters.Single(), reader.ReadToEnd() } };
            }
        }
        #endregion

        #region MapActionParameters
        private static void MapActionParameters(ActionExecutingContext filterContext, Func<string, Type, object> deserialize)
        {
            var input = CreateInputDictionary(filterContext);

            if (input == null)
                return;

            foreach (var param in filterContext.ActionDescriptor.GetParameters())
            {
                if (!input.ContainsKey(param.ParameterName))
                    continue;

                try
                {
                    filterContext.ActionParameters[param.ParameterName] = deserialize(input[param.ParameterName], param.ParameterType);
                }
                catch { }   // I don't like this anymore than you do, but I haven't found an effective
                // way of detecting if a parameter can/should be deserialized.
            }
        }
        #endregion

        #region OnActionExecuting
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            if (JsonTypes.Any(type => request.ContentType.Contains(type)))
                MapActionParameters(filterContext, (input, type) => JavascriptDeserialize.MakeGenericMethod(new[] { type }).Invoke(new JavaScriptSerializer(), new object[] { input }));

            else if (XmlTypes.Any(type => request.ContentType.Contains(type)))
                MapActionParameters(filterContext, (input, type) => new XmlSerializer(type).Deserialize(new StringReader(input)));
        }
        #endregion

        #region OnActionExecuted
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
                return;

            var acceptTypes = (filterContext.HttpContext.Request.AcceptTypes ?? new[] { "text/html" })
                .Select(a => TypeRegEx.Match(a).Value).ToArray();

            if (HtmlTypes.Any(acceptTypes.Contains))
                return;

            object model;

            if (filterContext.Controller.ViewData.ModelState.IsValid)
                model = filterContext.Controller.ViewData.Model;
            else
                model = filterContext.Controller.ViewData.ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => new { ErrorMessage = string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.ErrorMessage : e.Exception.GetMessage() })
                    .ToArray();

            if (JsonTypes.Any(acceptTypes.Contains))
                filterContext.Result = new JsonNetResult
                {
                    Data = model,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            else if (XmlTypes.Any(acceptTypes.Contains))
                filterContext.Result = new XmlResult { Data = model };
        }
        #endregion
    }
}