using System.Collections.Generic;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaMatriz : DomainEmpresaBase<FichaTecnicaMatriz>
    {
        private readonly IList<FichaTecnicaVariacaoMatriz> _fichaTecnicaVariacaoMatrizs = new List<FichaTecnicaVariacaoMatriz>();
        private readonly IList<FichaTecnicaMaterialConsumo> _materialConsumoMatrizs = new List<FichaTecnicaMaterialConsumo>();
        private readonly IList<FichaTecnicaMaterialConsumoVariacao> _materialConsumoItems = new List<FichaTecnicaMaterialConsumoVariacao>();

        public virtual Grade Grade { get; set; }

        public virtual IList<FichaTecnicaVariacaoMatriz> FichaTecnicaVariacaoMatrizs
        {
            get { return _fichaTecnicaVariacaoMatrizs; }
        }

        public virtual IList<FichaTecnicaMaterialConsumo> MaterialConsumoMatrizs
        {
            get { return _materialConsumoMatrizs; }
        }

        public virtual IList<FichaTecnicaMaterialConsumoVariacao> MaterialConsumoItems
        {
            get { return _materialConsumoItems; }
        }
    }
}