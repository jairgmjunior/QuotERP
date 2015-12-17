using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class EstoqueMaterial : DomainBase<EstoqueMaterial>
    {
        private readonly IList<MovimentacaoEstoqueMaterial> _movimentacaoEstoqueMateriais;

        public virtual double Quantidade { get; set; }
        public virtual double Reserva { get; set; }
        public virtual DepositoMaterial DepositoMaterial { get; set; }
        public virtual Material Material { get; set; }
        public virtual IList<EstoqueMaterial> EstoqueMateriais { get; set; }
       
        public EstoqueMaterial()
        {
            _movimentacaoEstoqueMateriais = new List<MovimentacaoEstoqueMaterial>();
        }

        #region MovimentacaoEstoqueMateriais
        public virtual IReadOnlyCollection<MovimentacaoEstoqueMaterial> MovimentacaoEstoqueMateriais
        {
            get { return new ReadOnlyCollection<MovimentacaoEstoqueMaterial>(_movimentacaoEstoqueMateriais); }
        }

        public virtual void AddMovimentacaoEstoqueMaterial(params MovimentacaoEstoqueMaterial[] movimentacaoEstoqueMateriais)
        {
            foreach (var movimentacaoEstoqueMaterial in movimentacaoEstoqueMateriais)
            {
                movimentacaoEstoqueMaterial.EstoqueMaterial = this;
                _movimentacaoEstoqueMateriais.Add(movimentacaoEstoqueMaterial);
            }
        }

        public virtual void RemoveMovimentacaoEstoqueMaterial(params MovimentacaoEstoqueMaterial[] movimentacaoEstoqueMateriais)
        {
            foreach (var movimentacaoEstoqueMaterial in movimentacaoEstoqueMateriais)
            {
                if (_movimentacaoEstoqueMateriais.Contains(movimentacaoEstoqueMaterial))
                    _movimentacaoEstoqueMateriais.Remove(movimentacaoEstoqueMaterial);
            }
        }
        #endregion

        #region AtualizarEstoque
        /// <summary>
        /// Atualiza o estoque deste depósito para este material, de acordo com a quantidade.
        /// Se positivo: adiciona, se negativo: remove.
        /// </summary>
        public static EstoqueMaterial AtualizarEstoque(IRepository<EstoqueMaterial> estoqueMaterialRepository, 
            DepositoMaterial depositoMaterial, Material material, double quantidade)
        {
            var estoqueMaterial = estoqueMaterialRepository.Get(p => p.Material.Id == material.Id
                    && p.DepositoMaterial.Id == depositoMaterial.Id);
            
            if (estoqueMaterial == null)
            {
                if (quantidade < 0)
                    throw new Exception(string.Format("Não é possível cadastrar o estoque de material para o {0} no depósito {1}, pois ficaria com quantidade negativa.", 
                        material.Descricao, depositoMaterial.Nome));

                estoqueMaterial = new EstoqueMaterial
                {
                    Material = material,
                    DepositoMaterial = depositoMaterial,
                    Quantidade = quantidade,
                    Reserva = 0
                };
                estoqueMaterial = estoqueMaterialRepository.Save(estoqueMaterial);
            }
            else
            {
                if (estoqueMaterial.Quantidade + quantidade < 0)
                {
                    var mensagem = string.Format("O valor do estoque não pode ficar negativo.\r\nMaterial: {0}\r\nDepósito: {1}\r\nQuantidade atual: {2}",
                            material.Descricao, depositoMaterial.Nome, estoqueMaterial.Quantidade);
                    throw new Exception(mensagem);
                }

                estoqueMaterial.Quantidade += quantidade;
                estoqueMaterialRepository.Update(estoqueMaterial);
            }

            return estoqueMaterial;
        }
        #endregion

        public virtual double ObtenhaSaldo(DateTime data)
        {
            var saldoEntrada =
                MovimentacaoEstoqueMateriais.Where(
                    x =>
                        x.TipoMovimentacaoEstoqueMaterial == TipoMovimentacaoEstoqueMaterial.Entrada &&
                        x.Data <= data).Sum(x => x.Quantidade);

            var saldoSaida =
                MovimentacaoEstoqueMateriais.Where(
                    x =>
                        x.TipoMovimentacaoEstoqueMaterial == TipoMovimentacaoEstoqueMaterial.Saida &&
                        x.Data <= data).Sum(x => x.Quantidade);

            return saldoEntrada - saldoSaida;
        }
    }
}