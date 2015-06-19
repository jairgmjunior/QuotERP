using System;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaModelagemMedidaItem : DomainEmpresaBase<FichaTecnicaModelagemMedidaItem>
    {
        public virtual Tamanho Tamanho{ get; set; }     
        public virtual Double Medida { get; set; }
    }
}