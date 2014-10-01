using System.Runtime.InteropServices;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class ValidatorExtensions
    {
        #region InscricaoEstadual
        /// <summary>
        /// Valida inscrição estadual de acordo com a uf.
        /// </summary>
        /// <param name="cInsc"></param>
        /// <param name="cUF"></param>
        /// <returns></returns>
        [DllImport("DllInscE32.dll")]
        private static extern int ConsisteInscricaoEstadual(string cInsc, string cUF);

        public static bool IsInscricaoEstadual(this string ie, string uf)
        {
            if (string.IsNullOrWhiteSpace(ie))
                return true;

            return ConsisteInscricaoEstadual(ie, uf) == 0;
        }
        #endregion
    }
}