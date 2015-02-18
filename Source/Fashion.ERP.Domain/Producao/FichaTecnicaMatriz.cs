using System.Collections.Generic;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaMatriz : DomainEmpresaBase<FichaTecnicaMatriz>
    {
        private IList<FichaTecnicaVariacaoMatriz> _fichaTecnicaVariacaoMatrizs = new List<FichaTecnicaVariacaoMatriz>();

        public virtual Grade Grade { get; set; }

        public virtual IList<FichaTecnicaVariacaoMatriz> FichaTecnicaVariacaoMatrizs
        {
            get { return _fichaTecnicaVariacaoMatrizs; }
            set { _fichaTecnicaVariacaoMatrizs = value; }
        } 
    }
}