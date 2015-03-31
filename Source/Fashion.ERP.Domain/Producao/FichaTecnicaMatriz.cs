using System.Collections.Generic;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaMatriz : DomainEmpresaBase<FichaTecnicaMatriz>
    {
        private readonly IList<FichaTecnicaVariacaoMatriz> _fichaTecnicaVariacaoMatrizs = new List<FichaTecnicaVariacaoMatriz>();
        private readonly IList<MaterialConsumoMatriz> _materialConsumoMatrizs = new List<MaterialConsumoMatriz>();
        private readonly IList<MaterialConsumoItem> _materialConsumoItems = new List<MaterialConsumoItem>();

        public virtual Grade Grade { get; set; }

        public virtual IList<FichaTecnicaVariacaoMatriz> FichaTecnicaVariacaoMatrizs
        {
            get { return _fichaTecnicaVariacaoMatrizs; }
        }

        public virtual IList<MaterialConsumoMatriz> MaterialConsumoMatrizs
        {
            get { return _materialConsumoMatrizs; }
        }

        public virtual IList<MaterialConsumoItem> MaterialConsumoItems
        {
            get { return _materialConsumoItems; }
        }
    }
}