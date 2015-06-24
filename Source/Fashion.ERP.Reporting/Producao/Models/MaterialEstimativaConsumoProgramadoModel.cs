using System;
using System.Collections.Generic;

namespace Fashion.ERP.Reporting.Producao.Models
{
    public class MaterialEstimativaConsumoProgramadoModel
    {
        public virtual string Referencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string UnidadeMedida { get; set; }
        public virtual double UltimoCusto { get; set; }
        public virtual DateTime? DataUltimoCusto { get; set; }
        public virtual double QuantidadeDisponivel { get; set; }
        public virtual double QuantidadeReservada { get; set; }
        public virtual double QuantidadeEstoque { get; set; }
        public virtual double QuantidadeCompras { get; set; }
        public IEnumerable<ProgramacaoEstimativaConsumoProgramadoModel> Programacoes { get; set; }
    }
}