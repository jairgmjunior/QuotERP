using System;
using System.Collections.Generic;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Base;

namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducao : DomainEmpresaBase<ProgramacaoProducao>, IPesquisavelPorData
    {
        private readonly IList<ProgramacaoProducaoMaterial> _programacaoProducaoMateriais = new List<ProgramacaoProducaoMaterial>();
        private readonly IList<ProgramacaoProducaoItem> _programacaoProducaoItems = new List<ProgramacaoProducaoItem>();

        public virtual long Lote { get; set; }
        public virtual long Ano { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataProgramada { get; set; }
        public virtual String Observacao { get; set; }
        public virtual long Quantidade { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual SituacaoProgramacaoProducao SituacaoProgramacaoProducao { get; set; }
        public virtual Pessoa Funcionario { get; set; }
        public virtual RemessaProducao RemessaProducao { get; set; }

        public virtual IList<ProgramacaoProducaoMaterial> ProgramacaoProducaoMateriais
        {
            get { return _programacaoProducaoMateriais; }
        }

        public virtual IList<ProgramacaoProducaoItem> ProgramacaoProducaoItems
        {
            get { return _programacaoProducaoItems; }
        }
    }
}