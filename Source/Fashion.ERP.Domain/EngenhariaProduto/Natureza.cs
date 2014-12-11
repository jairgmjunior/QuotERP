namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Natureza : DomainBase<Natureza>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}