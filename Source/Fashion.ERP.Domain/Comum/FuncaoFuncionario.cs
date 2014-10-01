using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Fashion.Framework.Common.Utils;

namespace Fashion.ERP.Domain.Comum
{
    [TypeConverter(typeof(EnumDisplayTypeConverter<FuncaoFuncionario>))]
    public enum FuncaoFuncionario
    {
        [Display(Name = "Caixa")]
        Caixa,
        [Display(Name = "Comprador")]
        Comprador,
        [Display(Name = "Gerente de compra")]
        GerenteCompra,
        [Display(Name = "Estoquista")]
        Estoquista,
        [Display(Name = "Gerente de loja")]
        GerenteLoja,
        [Display(Name = "Produção")]
        Producao,
        [Display(Name = "Supervisor de Loja")]
        SupervisorLoja,
        [Display(Name = "Vendedor")]
        Vendedor,
        [Display(Name = "Chefe de almoxarifado")]
        ChefeAlmoxarifado,
        [Display(Name = "Auxilar de almoxarifado")]
        AuxilarAlmoxarifado,
        [Display(Name = "Estilista")]
        Estilista,
        [Display(Name = "Modelista")]
        Modelista,
        [Display(Name = "Supervisor engenharia de produção")]
        SupervisorEngenhariaProdução,
        [Display(Name = "Programador bordado")]
        ProgramadorBordado,
    }

    public static class FuncaoFuncionarioExtensions
    {
        public static string EnumToString(this FuncaoFuncionario funcaoFuncionario)
        {
            var display = funcaoFuncionario.GetDisplay();
            return display != null ? display.Name : funcaoFuncionario.ToString();
        }
    }
}