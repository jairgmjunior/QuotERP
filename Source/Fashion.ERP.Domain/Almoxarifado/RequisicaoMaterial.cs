using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Base;
using Fashion.Framework.Repository;
using FluentNHibernate.Conventions;
using NHibernate.Linq;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class RequisicaoMaterial : DomainEmpresaBase<RequisicaoMaterial>, IPesquisavelPorData
    {
        private IList<ReservaMaterial> _reservaMateriais = new List<ReservaMaterial>();
        private IList<SaidaMaterial> _saidaMaterials = new List<SaidaMaterial>();
        private IList<RequisicaoMaterialItem> _requisicaoMaterialItems = new List<RequisicaoMaterialItem>();

        public virtual long Numero { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual String Origem { get; set; }
        public virtual String Observacao { get; set; }
        public virtual SituacaoRequisicaoMaterial SituacaoRequisicaoMaterial { get; set; }
        public virtual TipoItem TipoItem { get; set; }
        public virtual Pessoa Requerente { get; set; }
        public virtual Pessoa UnidadeRequerente { get; set; }
        public virtual Pessoa UnidadeRequisitada { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }
        //public virtual ReservaMaterial ReservaMaterial { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        
        public virtual IList<ReservaMaterial> ReservaMateriais
        {
            get { return _reservaMateriais; }
            set { _reservaMateriais = value; }
        }

        public virtual IList<SaidaMaterial> SaidaMaterials
        {
            get { return _saidaMaterials; }
            set { _saidaMaterials = value; }
        }

        public virtual IList<RequisicaoMaterialItem> RequisicaoMaterialItems
        {
            get { return _requisicaoMaterialItems; }
            set { _requisicaoMaterialItems = value; }
        }

        public virtual void AtualizeSituacao()
        {
            double quantidadeSolicitada = _requisicaoMaterialItems.ToList().Select(p => p.QuantidadeSolicitada)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

            double quantidadeAtendida = _requisicaoMaterialItems.ToList().Select(p => p.QuantidadeAtendida)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

            double quantidadeCancelada = _requisicaoMaterialItems.ToList().Select(
                p => p.RequisicaoMaterialItemCancelado == null ? 0 : p.RequisicaoMaterialItemCancelado.QuantidadeCancelada)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);
            if ((quantidadeAtendida + quantidadeCancelada) == 0)
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.NaoAtendido;
            else if (quantidadeSolicitada.Equals(quantidadeCancelada))
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.Cancelado;
            else if (quantidadeSolicitada <= (quantidadeAtendida + quantidadeCancelada))
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.AtendidoTotal;
            else if (quantidadeSolicitada > (quantidadeAtendida + quantidadeCancelada))
                SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.AtendidoParcial;

            RequisicaoMaterialItems.ForEach(x => x.AtualizeSituacao());
        }
        
        public virtual void CrieReservaMaterial(long numero, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository)
        {
            if (TipoItem.Descricao != "MATÉRIA-PRIMA")
            {
                return;
            }

            if (ReservaMateriais.Any())
                return;

            var reservaMaterial = new ReservaMaterial()
            {
                Data = DateTime.Now,
                ReferenciaOrigem = Origem,
                Observacao = Observacao,
                Requerente = Requerente,
                Numero = numero,
                Unidade = UnidadeRequisitada
            };

            RequisicaoMaterialItems.ForEach(x =>
            {
                var reservaMaterialItem = new ReservaMaterialItem
                {
                    Material = x.Material,
                    QuantidadeReserva = x.QuantidadeSolicitada,
                    SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida
                };

                reservaMaterial.ReservaMaterialItems.Add(reservaMaterialItem);
                ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(x.QuantidadeSolicitada, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
            });

            ReservaMateriais.Add(reservaMaterial);
        }

        public virtual void BaixeReservaMaterial(IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            Material material, double quantidadeBaixa)
        {
            if (!ReservaMateriais.Any())
            {
                return;
            }

            var reservaMaterialItem = ReservaMateriais.SelectMany(x => x.ReservaMaterialItems).FirstOrDefault(y => y.Material.Id == material.Id);

            if (reservaMaterialItem == null)
            {
                return;
            }

            reservaMaterialItem.Material = material;
            reservaMaterialItem.QuantidadeAtendida += quantidadeBaixa;

            ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(quantidadeBaixa * -1, material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
        }

        public virtual void CanceleReservaMaterial(IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            Material material, double quantidadeCancelada, string observacaoCancelamento)
        {
            if (!ReservaMateriais.Any())
            {
                return;
            }
            
            var reservasMaterialItem = ReservaMateriais.SelectMany(x => x.ReservaMaterialItems).Where(y => y.Material.Id == material.Id);

            reservasMaterialItem.ForEach(reservaMaterialItem =>
            {
                reservaMaterialItem.ReservaMaterialItemCancelado = new ReservaMaterialItemCancelado()
                {
                    Data = DateTime.Now,
                    QuantidadeCancelada = quantidadeCancelada,
                    Observacao = observacaoCancelamento
                };
            });

            ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(quantidadeCancelada * -1, material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
        }

        public virtual void AtualizeReservaMaterial(IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository)
        {
            if (TipoItem.Descricao != "MATÉRIA-PRIMA")
            {
                return;
            }

            ReservaMateriais.ForEach(reservaMaterial =>
            {
                reservaMaterial.ReferenciaOrigem = Origem;
                reservaMaterial.Observacao = Observacao;
                reservaMaterial.Unidade = UnidadeRequisitada;
                reservaMaterial.Requerente = Requerente;                
            });
            
            RequisicaoMaterialItems.ForEach(x =>
            {
                var reservaMaterialItem = ReservaMateriais.SelectMany(z => z.ReservaMaterialItems).FirstOrDefault(y => y.Material.Id == x.Material.Id);
                
                if (reservaMaterialItem == null)
                {
                    reservaMaterialItem = new ReservaMaterialItem
                    {
                        Material = x.Material,
                        QuantidadeReserva = x.QuantidadeSolicitada,
                        SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida
                    };

                    ReservaMateriais.First().ReservaMaterialItems.Add(reservaMaterialItem);
                    ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(x.QuantidadeSolicitada, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
                }
                else
                {
                    var diferenca = x.QuantidadeSolicitada - reservaMaterialItem.QuantidadeReserva;

                    reservaMaterialItem.Material = x.Material;
                    reservaMaterialItem.QuantidadeReserva = x.QuantidadeSolicitada;

                    ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(diferenca, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
                }
            });

            var reservaMaterialItemsAExcluir = new List<ReservaMaterialItem>();

            ReservaMateriais.SelectMany(z => z.ReservaMaterialItems).ForEach(x =>
            {
                var requisicaoMaterialItem = RequisicaoMaterialItems.FirstOrDefault(y => y.Material.Id == x.Material.Id);
                if (requisicaoMaterialItem == null)
                {
                    reservaMaterialItemsAExcluir.Add(x);
                }
            });

            reservaMaterialItemsAExcluir.ForEach(x =>
            {
                var reservaMaterial =
                    ReservaMateriais.FirstOrDefault(z => z.ReservaMaterialItems.Any(y => y.Material.Id == x.Material.Id));

                reservaMaterial.ReservaMaterialItems.Remove(x);
                ReservaEstoqueMaterial.AtualizeReservaEstoqueMaterial(x.QuantidadeReserva * -1, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
            });

            for (int i = 0; i < ReservaMateriais.Count; i++)
            {
                if (ReservaMateriais[i].ReservaMaterialItems.IsEmpty())
                {
                    ReservaMateriais.Remove(ReservaMateriais[i]);
                }
            }
        }
    }
}