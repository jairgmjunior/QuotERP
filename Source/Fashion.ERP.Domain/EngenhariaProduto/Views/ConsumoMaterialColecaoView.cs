namespace Fashion.ERP.Domain.EngenhariaProduto.Views
{
    public class ConsumoMaterialColecaoView : DomainBase<ConsumoMaterialColecaoView>
    {
        public virtual string Referencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Unidade { get; set; }
        public virtual double Quantidade { get; set; }
        public virtual double Compras { get; set; }
        public virtual double Estoque { get; set; }
        public virtual double Diferenca { get; set; }
        public virtual long? Colecao { get; set; }
        public virtual long? ColecaoAprovada { get; set; }
        public virtual long? Categoria { get; set; }
        public virtual long? Subcategoria { get; set; }
        public virtual long? Familia { get; set; }
    }
}