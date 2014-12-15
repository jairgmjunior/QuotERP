using System;
using System.Text.RegularExpressions;

namespace Fashion.ERP.Domain.Extensions
{
    public static class CpfCpnjExtensions
    {
        public static String FormateCpfCnpj(this string cpfCnpj)
        {
            if (String.IsNullOrEmpty(cpfCnpj))
                return cpfCnpj;
            
            cpfCnpj = Regex.Replace(cpfCnpj, @"\D", string.Empty);

            if (cpfCnpj.Length == 14)
            {
                return Convert.ToUInt64(cpfCnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            
            //if(cpfCnpj.Length == 11)
            //{
            return Convert.ToUInt64(cpfCnpj).ToString(@"000\.000\.000\-00");
            //}

            //throw new Exception("A cadeia de caracteres '"+ cpfCnpj +"' não corresponde a cpf ou cnpj.");
        }

        public static String DesformateCpfCnpj(this string cpfCnpj)
        {
            if (String.IsNullOrEmpty(cpfCnpj))
                return cpfCnpj;

            return Regex.Replace(cpfCnpj, @"\D", string.Empty).Trim();
        }
    }
}