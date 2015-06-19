using System.Collections.Generic;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaModelagemMedida : DomainEmpresaBase<FichaTecnicaModelagemMedida>
    {
        public virtual string DescricaoMedida { get; set; }
        private readonly IList<FichaTecnicaModelagemMedidaItem> _itens = new List<FichaTecnicaModelagemMedidaItem>();
        
        public virtual IList<FichaTecnicaModelagemMedidaItem> Itens
        {
            get { return _itens; }
        }
    }
}