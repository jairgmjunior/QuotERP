using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class SetorProducao : DomainBase<SetorProducao>
    {
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
    }
}