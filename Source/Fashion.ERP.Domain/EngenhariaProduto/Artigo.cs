namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Artigo : DomainBase<Artigo>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}