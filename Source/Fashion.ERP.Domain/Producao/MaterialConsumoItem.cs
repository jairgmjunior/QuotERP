using System;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class MaterialConsumoItem : DomainEmpresaBase<MaterialConsumoItem>
    {
        public virtual Double Custo { get; set; }
        public virtual Double Quantidade { get; set; }
        public virtual bool CompoeCusto { get; set; }
        public virtual Material Material { get; set; }
        public virtual Tamanho Tamanho { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
        public virtual FichaTecnicaVariacaoMatriz FichaTecnicaVariacaoMatriz { get; set; }
    }
}