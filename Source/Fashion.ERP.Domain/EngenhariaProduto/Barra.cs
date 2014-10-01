namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Barra : DomainBase<Barra>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}