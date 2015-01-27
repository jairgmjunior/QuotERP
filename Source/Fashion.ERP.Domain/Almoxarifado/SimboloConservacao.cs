
namespace Fashion.ERP.Domain.Almoxarifado
{
    public class SimboloConservacao : DomainBase<SimboloConservacao>
    {
        public virtual string Descricao { get; set; }
        public virtual CategoriaConservacao CategoriaConservacao { get; set; }
        public virtual Arquivo Foto { get; set; }
    }
}
