using System;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Domain.Almoxarifado
{
    public class ReservaEstoqueMaterial : DomainEmpresaBase<ReservaEstoqueMaterial>
    {
        public virtual Material Material { get; set; }
        public virtual Pessoa Unidade { get; set; }
        public virtual Double Quantidade { get; set; }

        public virtual void AtualizeQuantidade(double valorAdicional)
        {
            Quantidade += valorAdicional;
        }

        public static void AtualizeReservaEstoqueMaterial(double valorAdicional, Material material, Pessoa unidade, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository)
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
    }
}