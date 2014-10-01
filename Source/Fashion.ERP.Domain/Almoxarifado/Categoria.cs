namespace Fashion.ERP.Domain.Almoxarifado
{
    public class Categoria : DomainBase<Categoria>
    {
        public virtual string Nome { get; set; }
        public virtual string CodigoNcm { get; set; }
        public virtual GeneroCategoria GeneroCategoria { get; set; }
        public virtual TipoCategoria TipoCategoria { get; set; }
        public virtual bool Ativo { get; set; }
    }
}