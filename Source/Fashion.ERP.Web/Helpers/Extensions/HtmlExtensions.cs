using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Fashion.Framework.Common.Validators;
using Newtonsoft.Json;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class HtmlExtensions
    {
        #region ActionLinkAuth
        public static MvcHtmlString ActionLinkAuth(this HtmlHelper helper, string text, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var routes = new RouteValueDictionary(routeValues);
            var areaName = GetAreaName(routes);

            if (!PermissaoHelper.PossuiPermissao(actionName, controllerName, areaName))
                return new MvcHtmlString(string.Empty);

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            return BuildLinkAuthButton(url, text, htmlAttributes);
        }
        #endregion

        #region ActionLinkAuth

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper helper, string text, ActionResult action, object htmlAttributes = null, bool? enabled = null)
        {
            var result = action.GetT4MVCResult();
            var actionName = result.Action;
            var controllerName = result.Controller;
            var areaName = GetAreaName(action.GetRouteValueDictionary());

            if ((enabled ?? true) == false || PermissaoHelper.PossuiPermissao(actionName, controllerName, areaName) == false)
                return new MvcHtmlString(string.Empty);

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action);

            return BuildLinkAuthButton(url, text, htmlAttributes);
        }
        #endregion

        #region EditarAuth
        public static MvcHtmlString EditarAuth(this HtmlHelper helper, ActionResult action)
        {
            return ActionLinkAuth(helper, "", action, new { @class = "btn btn-small btn-primary btn-edit", @title="Editar" });
        }
        #endregion


        #region ExcluirAuth
        public static MvcHtmlString ExcluirAuth(this HtmlHelper helper, ActionResult action)
        {
            return ActionLinkAuth(helper, "Excluir", action, new { @class = "delete btn" });
        }
        #endregion

        #region CancelaAuth
        public static MvcHtmlString CancelaAuth(this HtmlHelper helper, ActionResult action)
        {
            return ActionLinkAuth(helper, "Cancelar", action, new { @class = "btn btn-small btn-primary" });
        }
        #endregion

        #region EditarSituacaoAuth
        public static MvcHtmlString EditarSituacaoAuth(this HtmlHelper helper, ActionResult action, bool ativo)
        {
            // Gerar o botão de acordo com a situação
            return ativo
                    ? ActionLinkAuth(helper, "", action, new { @class = "btn btn-small btn-danger btn-editar-situacao btn-inativar", @title = "Inativar" })
                    : ActionLinkAuth(helper, "", action, new { @class = "btn btn-small btn-success btn-editar-situacao btn-ativar", @title = "Ativar" });
        }
        #endregion

        #region GetAreaName
        private static string GetAreaName(RouteValueDictionary routeValueDictionary)
        {
            return routeValueDictionary["area"] as string;
        }
        #endregion

        #region AreaName
        public static string AreaName(this WebViewPage page)
        {
            if (page == null) throw new ArgumentNullException("page");

            var rd = page.Context.Request.RequestContext.RouteData;
            return (rd.Values["area"] ?? rd.DataTokens["area"]) as string;
        }
        #endregion

        #region ControllerName
        public static string ControllerName(this WebViewPage page)
        {
            if (page == null) throw new ArgumentNullException("page");
            return page.ViewContext.Controller.ValueProvider.GetValue("controller").AttemptedValue;
        }

        #endregion

        #region ActionName
        public static string ActionName(this WebViewPage page)
        {
            if (page == null) throw new ArgumentNullException("page");
            return page.ViewContext.Controller.ValueProvider.GetValue("action").AttemptedValue;
        }
        #endregion

        #region IsAction
        public static bool IsAction(this WebViewPage page, string actionName)
        {
            if (page == null) throw new ArgumentNullException("page");

            return page.ViewContext.Controller.ValueProvider.GetValue("action").AttemptedValue == actionName;
        }
        #endregion

        #region IsEditar
        public static bool IsEditar(this WebViewPage page)
        {
            return IsAction(page, "Editar");
        }
        #endregion

        #region IsIncluir
        public static bool IsNovo(this WebViewPage page)
        {
            return IsAction(page, "Novo");
        }
        #endregion

        #region BuildLinkAuthButton
        private static MvcHtmlString BuildLinkAuthButton(string url, string text, object htmlAttributes)
        {
            var buttonBuilder = new TagBuilder("a");
            buttonBuilder.Attributes.Add("href", url);
            buttonBuilder.SetInnerText(text);
            buttonBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(buttonBuilder.ToString());
        }
        #endregion

        #region BeginForm
        /// <summary>
        /// Inicia um form para uso com o bootstrap modal.
        /// </summary>
        /// <remarks>
        /// Depende das funções onAjaxFormSucess e onAjaxFormComplete definidas no utils.js.
        /// </remarks>
        /// <param name="ajaxHelper">Helper.</param>
        /// <param name="result">ActionResult.</param>
        /// <param name="modalName">Id do div que contém o bootstrap modal.</param>
        public static MvcForm BeginForm(this AjaxHelper ajaxHelper, ActionResult result, string modalName)
        {
            var callInfo = result.GetT4MVCResult();
            var ajaxOptions = new AjaxOptions { OnSuccess = "onAjaxFormSucess('" + modalName + "')", OnComplete = "onAjaxFormComplete", HttpMethod = "POST" };
            var htmlAttributes = new Dictionary<string, object> { { "class", "form-horizontal" } };
            return ajaxHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary, ajaxOptions, htmlAttributes);
        }
        #endregion

        #region BeginSearchForm
        /// <summary>
        /// Inicia um form para a pesquisa modal.
        /// </summary>
        /// <param name="ajaxHelper">Ajax helper.</param>
        /// <param name="result">ActionResult.</param>
        /// <param name="nomePesquisa">Nome da pesquisa.</param>
        public static MvcForm BeginSearchForm(this AjaxHelper ajaxHelper, ActionResult result, string nomePesquisa)
        {
            var callInfo = result.GetT4MVCResult();
            var ajaxOptions = new AjaxOptions { UpdateTargetId = "modal-body-" + nomePesquisa };
            var htmlAttributes = new Dictionary<string, object> { { "class", "form-inline" } };
            return ajaxHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary, ajaxOptions, htmlAttributes);
        }
        #endregion

        #region LabelForRequired
        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "", object htmlAttributes = null)
        {
            return LabelHelper(html,
                ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                ExpressionHelper.GetExpressionText(expression), labelText,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelHelper(html,
                ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                ExpressionHelper.GetExpressionText(expression),
                null,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        #endregion

        #region LabelHelper
        private static MvcHtmlString LabelHelper(HtmlHelper html,
            ModelMetadata metadata, string htmlFieldName, string labelText,
            IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(labelText))
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (string.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            bool isRequired = false;

            if (metadata.ContainerType != null && metadata.PropertyName != null)
                isRequired = metadata.ContainerType.GetProperty(metadata.PropertyName)
                                 .GetCustomAttributes(typeof(RequiredAttribute), false)
                                 .Length == 1 ||
                             metadata.ContainerType.GetProperty(metadata.PropertyName)
                                 .GetCustomAttributes(typeof(RequiredIfAttribute), false)
                                 .Length >= 1;

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.Attributes.Add("class", "control-label");

            if (isRequired)
                tag.Attributes["class"] += " required-label";

            tag.SetInnerText(labelText);
            tag.TrueMergeAttributes(htmlAttributes);

            var output = tag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(output);
        }
        #endregion

        #region UneditableTextBoxFor
        public static MvcHtmlString UneditableTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var model = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model;

            var tag = new TagBuilder("span");
            tag.SetInnerText(html.FormatValue(model, null));
            tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);

            if (tag.Attributes.ContainsKey("class"))
                tag.Attributes["class"] += " uneditable-input";
            else
                tag.Attributes.Add("class", "uneditable-input");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        #endregion

        #region UneditableTextBoxFor
        public static MvcHtmlString UneditableTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var model = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model;

            var tag = new TagBuilder("div");
            tag.SetInnerText(html.FormatValue(model, null));
            tag.Attributes.Add("rows", "3");
            tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);

            if (tag.Attributes.ContainsKey("class"))
                tag.Attributes["class"] += " input-large uneditable-textarea";
            else
                tag.Attributes.Add("class", "input-large uneditable-textarea");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        #endregion

        #region GetUrl
        /// <summary>
        /// Retorna uma url relativa à página.
        /// </summary>
        public static string GetUrl(this HtmlHelper helper, ActionResult action)
        {
            return new UrlHelper(helper.ViewContext.RequestContext).Action(action);
        }
        #endregion

        #region ToJson
        public static MvcHtmlString ToJson(this HtmlHelper html, object obj)
        {
            var scriptSerializer = JsonSerializer.Create();

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, obj);
                return MvcHtmlString.Create(sw.ToString());
            }
        }
        #endregion
    }
}