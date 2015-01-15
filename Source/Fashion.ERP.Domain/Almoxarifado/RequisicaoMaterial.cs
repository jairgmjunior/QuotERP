using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Repository;
using NHibernate.Linq;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class RequisicaoMaterial : DomainEmpresaBase<RequisicaoMaterial>
    {
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
        public virtual ReservaMaterial ReservaMaterial { get; set; }

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
            if (ReservaMaterial != null)
                return;

            ReservaMaterial = new ReservaMaterial()
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

                ReservaMaterial.ReservaMaterialItems.Add(reservaMaterialItem);
                ReservaMaterial.AtualizeReservaEstoqueMaterial(x.QuantidadeSolicitada, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
            });
        }

        public virtual void AtualizeReservaMaterial(IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository)
        {
            ReservaMaterial.ReferenciaOrigem = Origem;
            ReservaMaterial.Observacao = Observacao;
            ReservaMaterial.Unidade = UnidadeRequisitada;
            ReservaMaterial.Requerente = Requerente;
            
            RequisicaoMaterialItems.ForEach(x =>
            {
                var reservaMaterialItem = ReservaMaterial.ReservaMaterialItems.FirstOrDefault(y => y.Material.Id == x.Material.Id);

                if (reservaMaterialItem == null)
                {
                    reservaMaterialItem = new ReservaMaterialItem
                    {
                        Material = x.Material,
                        QuantidadeReserva = x.QuantidadeSolicitada,
                        SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida
                    };

                    ReservaMaterial.ReservaMaterialItems.Add(reservaMaterialItem);
                    ReservaMaterial.AtualizeReservaEstoqueMaterial(x.QuantidadeSolicitada, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
                }
                else
                {
                    var diferenca = x.QuantidadeSolicitada - reservaMaterialItem.QuantidadeReserva;

                    reservaMaterialItem.Material = x.Material;
                    reservaMaterialItem.QuantidadeReserva = x.QuantidadeSolicitada;
                    
                    ReservaMaterial.AtualizeReservaEstoqueMaterial(diferenca, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
                }
            });

            var reservaMaterialItemsAExcluir = new List<ReservaMaterialItem>();

            ReservaMaterial.ReservaMaterialItems.ForEach(x =>
            {
                var requisicaoMaterialItem = RequisicaoMaterialItems.FirstOrDefault(y => y.Material.Id == x.Material.Id);
                if (requisicaoMaterialItem == null)
                {
                    reservaMaterialItemsAExcluir.Add(x);
                }
            });

            reservaMaterialItemsAExcluir.ForEach(x =>
            {
                ReservaMaterial.ReservaMaterialItems.Remove(x);
                ReservaMaterial.AtualizeReservaEstoqueMaterial(x.QuantidadeReserva * -1, x.Material, UnidadeRequisitada, reservaEstoqueMaterialRepository);
            });
        }
    }
}