namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Segmento : DomainBase<Segmento>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}