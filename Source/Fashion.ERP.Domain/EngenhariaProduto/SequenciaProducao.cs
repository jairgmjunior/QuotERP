using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class SequenciaProducao : DomainBase<SequenciaProducao>
    {
        private IList<MaterialComposicaoModelo> _materialComposicaoModelos = new List<MaterialComposicaoModelo>();
        
        public virtual DateTime? DataEntrada { get; set; }
        public virtual DateTime? DataSaida { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
        public virtual SetorProducao SetorProducao { get; set; }

        public virtual IList<MaterialComposicaoModelo> MaterialComposicaoModelos
        {
            get { return _materialComposicaoModelos; }
            set { _materialComposicaoModelos = value; }
        }
    }
}