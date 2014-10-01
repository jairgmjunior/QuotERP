namespace Fashion.ERP.Domain.Almoxarifado
{
    public class MarcaMaterial : DomainBase<MarcaMaterial>
    {
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
    }
}