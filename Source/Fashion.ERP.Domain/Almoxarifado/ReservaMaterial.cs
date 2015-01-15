using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Repository;
using NHibernate.Linq;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaMaterial : DomainEmpresaBase<ReservaMaterial>
    {
        private IList<ReservaMaterialItem> _reservaMaterialItems = new List<ReservaMaterialItem>();

        public virtual long Numero { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime? DataProgramacao { get; set; }
        public virtual String Observacao { get; set; }
        public virtual Pessoa Requerente { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual Colecao Colecao { get; set; }
        public virtual String ReferenciaOrigem { get; set; }
        public virtual SituacaoReservaMaterial SituacaoReservaMaterial { get; set; }

        public virtual IList<ReservaMaterialItem> ReservaMaterialItems
        {
            get { return _reservaMaterialItems; }
            set { _reservaMaterialItems = value; }
        }

        public virtual void AtualizeSituacao()
        {
            double quantidadeReserva = _reservaMaterialItems.ToList().Select(p => p.QuantidadeReserva)  
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

            double quantidadeAtendida = _reservaMaterialItems.ToList().Select(p => p.QuantidadeAtendida)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);

            double quantidadeCancelada = _reservaMaterialItems.ToList().Select(
                p => p.ReservaMaterialItemCancelado == null ? 0 : p.ReservaMaterialItemCancelado.QuantidadeCancelada)
                .Aggregate((quantidadeTotal, quantidade) => quantidadeTotal + quantidade);
            if ((quantidadeAtendida + quantidadeCancelada) == 0)
                SituacaoReservaMaterial = SituacaoReservaMaterial.NaoAtendida;
            else if (quantidadeReserva.Equals(quantidadeCancelada))
                SituacaoReservaMaterial = SituacaoReservaMaterial.Cancelada;
            else if (quantidadeReserva <= (quantidadeAtendida + quantidadeCancelada))
                SituacaoReservaMaterial = SituacaoReservaMaterial.AtendidaTotal;
            else if (quantidadeReserva > (quantidadeAtendida + quantidadeCancelada))
                SituacaoReservaMaterial = SituacaoReservaMaterial.AtendidaParcial;

            ReservaMaterialItems.ForEach(x => x.AtualizeSituacao());
        }

        public virtual void AtualizeReservaEstoqueMaterial(double valorAdicional, Material material, Pessoa unidade, 
            IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository)
        {
            var reservaEstoqueMaterial = reservaEstoqueMaterialRepository.Find(x => x.Material.Id == material.Id && x.Unidade.Id == unidade.Id).FirstOrDefault();

            if (reservaEstoqueMaterial == null)
            {
                reservaEstoqueMaterial = new ReservaEstoqueMaterial
                {
                    Material = material,
                    Unidade = unidade
                };
            }

            reservaEstoqueMaterial.AtualizeQuantidade(valorAdicional);

            reservaEstoqueMaterialRepository.SaveOrUpdate(reservaEstoqueMaterial);
        }

        public virtual void AtualizeReservaEstoqueMaterialAoExcluir(IRepository<ReservaEstoqueMaterial> reservaEstoquematerialRepository)
        {
            ReservaMaterialItems.ForEach(x =>
            {
                var reservaEstoqueMaterial = reservaEstoquematerialRepository.Find(y => y.Material.Id == x.Material.Id && y.Unidade.Id == Unidade.Id).FirstOrDefault();

                if (reservaEstoqueMaterial != null)
                {
                    reservaEstoqueMaterial.AtualizeQuantidade(x.QuantidadeReserva * -1);

                    reservaEstoquematerialRepository.SaveOrUpdate(reservaEstoqueMaterial);
                }
            });
        }
    }
}