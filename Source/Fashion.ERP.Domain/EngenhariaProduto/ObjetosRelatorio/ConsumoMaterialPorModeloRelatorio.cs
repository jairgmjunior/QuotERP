using System.Collections.Generic;

namespace Fashion.ERP.Domain.EngenhariaProduto.ObjetosRelatorio
{
    public class ConsumoMaterialPorModeloRelatorio
    {
        public virtual string Referencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double Custo { get; set; }
        public virtual string NomeFoto { get; set; }
        public virtual double TotalQuantidadeMaterial { get; set; }
        public virtual double TotalQuantidadeAprovada { get; set; }
        public virtual double TotalQuantidadeTotalMaterial { get; set; }
        public virtual string UnidadeMedida { get; set; }
        public virtual double QuantidadeDisponivel { get; set; }
        public IEnumerable<ModeloConsumoMaterialRelatorio> Modelos { get; set; }
    }
}