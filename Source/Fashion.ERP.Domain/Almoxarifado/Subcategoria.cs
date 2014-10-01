namespace Fashion.ERP.Domain.Almoxarifado
{
    public class Subcategoria : DomainBase<Subcategoria>
    {
        public virtual string Nome { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual bool Ativo { get; set; }
    }
}