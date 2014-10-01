namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ProdutoBase : DomainBase<ProdutoBase>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}