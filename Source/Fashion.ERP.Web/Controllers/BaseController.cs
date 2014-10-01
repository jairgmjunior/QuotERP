using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Helpers.ActionResults;
using Fashion.ERP.Web.Models;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Controllers
{
    public abstract partial class BaseController : Controller
    {
        // usado para conter as informações que serão exibidas nas dropdownlist de ordenação e agrupamento.
        protected Dictionary<string, string> _colunasPesquisa;

        #region Virtuals
        protected virtual void ValidaNovoOuEditar(IModel model, string actionName) { }
        protected virtual void ValidaExcluir(long id) { }
        #endregion

        #region OnActionExecuting
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Envia formulário para validação
            if (Request.HttpMethod == "POST")
            {
                // Novo ou Editar
                if (filterContext.ActionParameters.ContainsKey("model") &&
                    new[] { "Novo", "Editar", "NovoOuEditar" }.Contains(filterContext.ActionDescriptor.ActionName))
                {
                    var model = filterContext.ActionParameters["model"] as IModel;

                    if (model != null)
                        ValidaNovoOuEditar(model, filterContext.ActionDescriptor.ActionName);
                }

                // Excluir
                if (filterContext.ActionParameters.ContainsKey("id") &&
                    filterContext.ActionDescriptor.ActionName == "Excluir")
                {
                    var id = (long)filterContext.ActionParameters["id"];
                    ValidaExcluir(id);
                }
            }
        }
        #endregion

        #region HandleUnknownAction
        protected override void HandleUnknownAction(string actionName)
        {
            try
            {
                // Executa uma View como partial para não carregar o Layout
                PartialView(actionName).ExecuteResult(ControllerContext);
            }
            catch (InvalidOperationException)
            {
                base.HandleUnknownAction(actionName);
            }
        }
        #endregion

        #region Json(object, string, Encoding, JsonRequestBehavior)
        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult"/> object that serializes the specified object to JavaScript Object Notation (JSON) format using the content type, content encoding, and the JSON request behavior.
        /// </summary>
        /// 
        /// <returns>
        /// The result object that serializes the specified object to JSON format.
        /// </returns>
        /// <param name="data">The JavaScript object graph to serialize.</param><param name="contentType">The content type (MIME type).</param><param name="contentEncoding">The content encoding.</param><param name="behavior">The JSON request behavior </param>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
                   {
                       Data = data,
                       ContentType = contentType,
                       ContentEncoding = contentEncoding,
                       JsonRequestBehavior = behavior
                   };
        }
        #endregion

        #region File
        /// <summary>
        /// Sobrecarrega o método File(fileName, contentType) adicionando o contentType automaticamente.
        /// </summary>
        protected FileResult File(string fileName)
        {
            return File(fileName, MimeMapping.GetMimeMapping(fileName));
        }
        #endregion

        #region Relatorio
        protected void MonteRelatorio(IPesquisaModel pesquisaModel, StringBuilder filtros, Report report)
        {
            if (filtros.Length > 2)
                report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

            if (pesquisaModel.AgruparPor != null)
            {
                grupo.Groupings.Add("=Fields." + pesquisaModel.AgruparPor);

                var key = _colunasPesquisa.First(p => p.Value == pesquisaModel.AgruparPor).Key;
                var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, pesquisaModel.AgruparPor);
                grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
            }
            else
            {
                report.Groups.Remove(grupo);
            }

            if (pesquisaModel.OrdenarPor != null)
                report.Sortings.Add("=Fields." + pesquisaModel.OrdenarPor,
                    pesquisaModel.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);
        }
        #endregion
    }
}