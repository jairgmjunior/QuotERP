using System;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducaoMaterial : DomainEmpresaBase<ProgramacaoProducaoMaterial>
    {
        public virtual double Quantidade { get; set; }
        public virtual ReservaMaterial ReservaMaterial { get; set; }
        public virtual Material Material { get; set; }
        public virtual Pessoa Responsavel { get; set; }
        public virtual Boolean Requisitado { get; set; }
        public virtual Boolean Reservado { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
    }
}