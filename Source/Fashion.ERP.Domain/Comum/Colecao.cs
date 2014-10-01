namespace Fashion.ERP.Domain.Comum
{
    public class Colecao : DomainBase<Colecao>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}