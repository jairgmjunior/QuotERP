using System.Collections.Generic;

namespace Fashion.ERP.Domain.Producao.ObjetosRelatorio
{
    public class ConsumoMaterialProducaoRelatorio
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
        public IEnumerable<FichaTecnicaConsumoMaterialRelatorio> FichasTecnicas { get; set; }
    }
}