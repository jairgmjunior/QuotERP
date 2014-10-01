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
    }
}