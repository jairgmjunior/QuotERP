using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class MessageExtensions
    {
        #region Variáveis
        private const string SuccessKey = "__successMessage";
        private const string InfoKey = "__infoMessage";
        private const string ErrorKey = "__errorMessage";
        #endregion

        #region SuccessMessage

        public static void AddSuccessMessage(this ControllerBase controller, string message, params object[] args)
        {
            var messages = new List<string>();

            if (!IsEmpty(controller.TempData, SuccessKey))
                messages = (List<string>)controller.TempData[SuccessKey];

            messages.Add(string.Format(message, args));

            controller.TempData[SuccessKey] = messages;
        }

        public static bool HasSuccessMessage(this WebViewPage webViewPage)
        {
            return !IsEmpty(webViewPage.TempData, SuccessKey);
        }

        public static List<string> GetSuccessMessage(this WebViewPage webViewPage)
        {
            return webViewPage.TempData[SuccessKey] as List<string>;
        }

        public static void ClearSuccessMessages(this ControllerBase controller)
        {
            controller.TempData[SuccessKey] = null;
        }

        #endregion

        #region InfoMessage

        public static void AddInfoMessage(this ControllerBase controller, string message, params object[] args)
        {
            var messages = new List<string>();

            if (!IsEmpty(controller.TempData, InfoKey))
                messages = (List<string>)controller.TempData[InfoKey];

            messages.Add(string.Format(message, args));

            controller.TempData[InfoKey] = messages;
        }

        public static bool HasInfoMessage(this WebViewPage webViewPage)
        {
            return !IsEmpty(webViewPage.TempData, InfoKey);
        }

        public static List<string> GetInfoMessage(this WebViewPage webViewPage)
        {
            return webViewPage.TempData[InfoKey] as List<string>;
        }

        public static void ClearInfoMessages(this ControllerBase controller)
        {
            controller.TempData[InfoKey] = null;
        }

        #endregion

        #region ErrorMessage

        public static void AddErrorMessage(this ControllerBase controller, string message, params object[] args)
        {
            var messages = new List<string>();

            if (!IsEmpty(controller.TempData, ErrorKey))
                messages = (List<string>)controller.TempData[ErrorKey];

            messages.Add(string.Format(message, args));

            controller.TempData[ErrorKey] = messages;
        }

        public static bool HasErrorMessage(this WebViewPage webViewPage)
        {
            return !IsEmpty(webViewPage.TempData, ErrorKey);
        }

        public static List<string> GetErrorMessage(this WebViewPage webViewPage)
        {
            return webViewPage.TempData[ErrorKey] as List<string>;
        }

        public static void ClearErrorMessages(this ControllerBase controller)
        {
            controller.TempData[ErrorKey] = null;
        }

        #endregion

        #region IsEmpty
        private static bool IsEmpty(IDictionary<string, object> tempData, string key)
        {
            return tempData[key] == null || tempData.All(t => t.Key != key);
        }
        #endregion
    }
}