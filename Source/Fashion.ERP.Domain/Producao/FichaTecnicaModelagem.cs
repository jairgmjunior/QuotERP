using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaModelagem: DomainEmpresaBase<FichaTecnicaModelagem>
    {
        private readonly IList<FichaTecnicaModelagemMedida> _medidas = new List<FichaTecnicaModelagemMedida>();
        
        public virtual Arquivo Arquivo { get; set; }
        public virtual string Observacao { get; set; }
        public virtual DateTime DataModelagem { get; set; }
        public virtual Pessoa Modelista{ get; set; }

        public virtual IList<FichaTecnicaModelagemMedida> Medidas
        {
            get { return _medidas; }
        }
    }
}