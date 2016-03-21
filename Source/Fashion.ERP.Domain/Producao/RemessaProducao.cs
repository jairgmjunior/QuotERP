using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Base;

namespace Fashion.ERP.Domain.Producao
{
    public class RemessaProducao : DomainEmpresaBase<RemessaProducao>, IPesquisavelPorData
    {
        private IList<RemessaProducaoCapacidadeProdutiva> _remessaProducaoCapacidadesProdutivas = new List<RemessaProducaoCapacidadeProdutiva>();

        public virtual long Numero { get; set; }
        public virtual long Ano { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Observacao { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataLimite { get; set; }
        public virtual Colecao Colecao { get; set; }

        public virtual IList<RemessaProducaoCapacidadeProdutiva> RemessasProducaoCapacidadesProdutivas
        {
            get { return _remessaProducaoCapacidadesProdutivas; }
            set { _remessaProducaoCapacidadesProdutivas = value; }
        }
    }
}