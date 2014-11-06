using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class UnidadeMedida : DomainBase<UnidadeMedida>
    {
        public virtual string Sigla { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double FatorMultiplicativo { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual Cor Cor { get; set; }

        public virtual double ConvertaQuantidadeParaEntrada(double quantidade)
        {
            return quantidade * ObtenhaFatorMultiplicativoParaEntrada();
        }

        public virtual double ObtenhaFatorMultiplicativoParaEntrada()
        {
            return FatorMultiplicativo < 1 ? 1 : FatorMultiplicativo;
        }
    }
}