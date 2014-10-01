using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class MaterialComposicaoModelo : DomainBase<MaterialComposicaoModelo>
    {
        public virtual double Quantidade { get; set; }

        public virtual UnidadeMedida UnidadeMedida { get; set; }
        public virtual Material Material { get; set; }
        public virtual Tamanho Tamanho { get; set; }
        public virtual Cor Cor { get; set; }
        public virtual Variacao Variacao { get; set; }
    }
}