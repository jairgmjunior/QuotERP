using System;

namespace Fashion.ERP.Domain.Producao.ObjetosRelatorio
{
    public class FichaTecnicaConsumoMaterialRelatorio
    {
        public virtual long Lote { get; set; }
        public virtual long Ano { get; set; }
        public virtual DateTime DataProgramada { get; set; }
        public virtual double Quantidade { get; set; }
        public virtual double QuantidadeAprovada { get; set; }
        public virtual double QuantidadeMaterial { get; set; }
        public virtual double QuantidadeTotalMaterial { get; set; } 
    }
}