using System.Text.RegularExpressions;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class ExceptionExtensions
    {
        #region ObtenhaNomeTabelaDependente
        public static string ObtenhaNomeTabelaDependente(this string mensagem)
        {
            var regex = new Regex(".tabela \"dbo.*?\"");
            return regex.IsMatch(mensagem) ? regex.Match(mensagem).Value.Replace(" tabela \"dbo.", "").Replace("\"", "") : "";
        }
        #endregion
    }
}