namespace Fashion.ERP.Domain.Comum
{
    public class Tamanho : DomainBase<Tamanho>
    {
        public virtual string Descricao { get; set; }
        public virtual string Sigla { get; set; }
        public virtual bool Ativo { get; set; }
    }
}