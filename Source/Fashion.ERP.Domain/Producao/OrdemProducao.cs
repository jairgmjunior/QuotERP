using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class OrdemProducao : DomainEmpresaBase<OrdemProducao>
    {
        private readonly IList<OrdemProducaoItem> _ordemProducaoItens;
        private readonly IList<OrdemProducaoMaterial> _ordemProducaoMateriais;

        public OrdemProducao()
        {
            _ordemProducaoItens = new List<OrdemProducaoItem>();
            _ordemProducaoMateriais = new List<OrdemProducaoMaterial>();
        }

        public virtual long Numero { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime DataProgramacao { get; set; }
        public virtual DateTime DataPrevisao { get; set; }
        public virtual string Observacao { get; set; }
        public virtual TipoOrdemProducao TipoOrdemProducao { get; set; }
        public virtual SituacaoOrdemProducao SituacaoOrdemProducao { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual FichaTecnicaMatriz FichaTecnicaMatriz { get; set; }

        #region OrdemProducaoFluxoBasico

        /// <summary>
        /// Lista com o fluxo de andamento ordenado.
        /// </summary>
        public virtual IDictionary<OrdemProducaoAndamentoFluxo, int> OrdemProducaoFluxoBasico { get; set; }

        #endregion

        #region OrdemProducaoItens

        public virtual IReadOnlyCollection<OrdemProducaoItem> OrdemProducaoItens
        {
            get { return new ReadOnlyCollection<OrdemProducaoItem>(_ordemProducaoItens); }
        }

        public virtual void AddOrdemProducaoItem(params OrdemProducaoItem[] ordemProducaoItens)
        {
            foreach (var ordemProducaoItem in ordemProducaoItens)
                if (!_ordemProducaoItens.Contains(ordemProducaoItem))
                {
                    ordemProducaoItem.OrdemProducao = this;
                    _ordemProducaoItens.Add(ordemProducaoItem);
                }
        }

        public virtual void RemoveOrdemProducaoItem(params OrdemProducaoItem[] ordemProducaoItens)
        {
            foreach (var ordemProducaoItem in ordemProducaoItens)
                if (_ordemProducaoItens.Contains(ordemProducaoItem))
                    _ordemProducaoItens.Remove(ordemProducaoItem);
        }

        #endregion

        #region OrdemProducaoMateriais

        public virtual IReadOnlyCollection<OrdemProducaoMaterial> OrdemProducaoMateriais
        {
            get { return new ReadOnlyCollection<OrdemProducaoMaterial>(_ordemProducaoMateriais); }
        }

        public virtual void AddOrdemProducaoItem(params OrdemProducaoMaterial[] ordemProducaoMateriais)
        {
            foreach (var ordemProducaoMaterial in ordemProducaoMateriais)
                if (!_ordemProducaoMateriais.Contains(ordemProducaoMaterial))
                {
                    ordemProducaoMaterial.OrdemProducao = this;
                    _ordemProducaoMateriais.Add(ordemProducaoMaterial);
                }
        }

        public virtual void RemoveOrdemProducaoItem(params OrdemProducaoMaterial[] ordemProducaoMateriais)
        {
            foreach (var ordemProducaoMaterial in ordemProducaoMateriais)
                if (_ordemProducaoMateriais.Contains(ordemProducaoMaterial))
                    _ordemProducaoMateriais.Remove(ordemProducaoMaterial);
        }

        #endregion
    }
}