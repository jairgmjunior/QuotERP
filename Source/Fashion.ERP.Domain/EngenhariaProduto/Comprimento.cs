namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Comprimento : DomainBase<Comprimento>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}