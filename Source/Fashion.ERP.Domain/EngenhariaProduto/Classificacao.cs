namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Classificacao : DomainBase<Classificacao>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}