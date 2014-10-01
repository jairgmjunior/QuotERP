namespace Fashion.ERP.Domain.Almoxarifado
{
    public class Familia : DomainBase<Familia>
    {
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
    }
}