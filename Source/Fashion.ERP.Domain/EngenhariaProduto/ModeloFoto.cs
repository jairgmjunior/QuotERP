namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloFoto : DomainBase<ModeloFoto>
    {
        public virtual bool Impressao { get; set; }
        public virtual bool Padrao { get; set; }
        public virtual Arquivo Foto { get; set; }
        public virtual Modelo Modelo { get; set; }
    }
}