using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class Material : DomainBase<Material>
    {
        private readonly IList<ReferenciaExterna> _referenciaExternas;
        private IList<CustoMaterial> _custoMaterials = new List<CustoMaterial>();

        public Material()
        {
            _referenciaExternas = new List<ReferenciaExterna>();
        }

        public virtual string Referencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Detalhamento { get; set; }
        public virtual string CodigoBarra { get; set; }
        public virtual string Ncm { get; set; }
        public virtual double Aliquota { get; set; }
        public virtual double PesoBruto { get; set; }
        public virtual double PesoLiquido { get; set; }
        public virtual string Localizacao { get; set; }
        public virtual bool Ativo { get; set; }

        public virtual OrigemSituacaoTributaria OrigemSituacaoTributaria { get; set; }
        public virtual Arquivo Foto { get; set; }
        public virtual GeneroFiscal GeneroFiscal { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }
        public virtual MarcaMaterial MarcaMaterial { get; set; }
        public virtual Familia Familia { get; set; }
        public virtual TipoItem TipoItem { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
        //todo remover essa propriedade
        public virtual Bordado Bordado { get; set; }
        public virtual Tecido Tecido { get; set; }

        #region ReferenciaExternas

        public virtual IReadOnlyCollection<ReferenciaExterna> ReferenciaExternas
        {
            get { return new ReadOnlyCollection<ReferenciaExterna>(_referenciaExternas); }
        }

        public virtual void AddReferenciaExterna(params ReferenciaExterna[] referenciaExternas)
        {
            foreach (var referenciaExterna in referenciaExternas)
            {
                referenciaExterna.Material = this;
                _referenciaExternas.Add(referenciaExterna);
            }
        }

        public virtual void RemoveReferenciaExterna(params ReferenciaExterna[] referenciaExternas)
        {
            foreach (var referenciaExterna in referenciaExternas)
            {
                if (_referenciaExternas.Contains(referenciaExterna))
                    _referenciaExternas.Remove(referenciaExterna);
            }
        }

        public virtual void ClearReferenciaExterna()
        {
            _referenciaExternas.Clear();
        }

        #endregion

        public virtual IList<CustoMaterial> CustoMaterials
        {
            get { return _custoMaterials; }
            set { _custoMaterials = value; }
        }

        public virtual void AtualizeCustoAtual()
        {
            var custoAtual = _custoMaterials.FirstOrDefault(x => x.Ativo);
            if (custoAtual != null)
            {
                custoAtual.Ativo = false;
            }

            var novoCustoAtual = _custoMaterials.OrderBy(x => x.Custo).ThenBy(x => x.Data).Last();
            novoCustoAtual.Ativo = true;
        }

        public virtual void ExcluaCusto(CustoMaterial custoMaterial, IRepository<Material> materialRepository)
        {
            CustoMaterials.Remove(custoMaterial);

            materialRepository.SaveOrUpdate(this);
        }
    }
}