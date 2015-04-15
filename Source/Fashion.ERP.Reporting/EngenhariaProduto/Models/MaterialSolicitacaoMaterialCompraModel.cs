using System;
using System.Collections.Generic;

namespace Fashion.ERP.Reporting.EngenhariaProduto.Models
{
    public class MaterialSolicitacaoMaterialCompraModel
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
        public IEnumerable<ProgramacaoSolicitacaoMaterialCompraModel> Programacoes { get; set; }
    }
}