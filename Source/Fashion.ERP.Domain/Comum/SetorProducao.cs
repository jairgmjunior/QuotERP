
namespace Fashion.ERP.Domain.Comum
{
    public class SetorProducao : DomainBase<SetorProducao>
    {
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
    }
}