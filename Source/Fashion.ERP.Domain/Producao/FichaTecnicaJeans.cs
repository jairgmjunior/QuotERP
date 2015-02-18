using System;
using Fashion.ERP.Domain.EngenhariaProduto;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaJeans : FichaTecnica
    {
        public virtual String Lavada { get; set; }
        public virtual String Pesponto { get; set; }
        public virtual String Cos { get; set; }
        public virtual String Passante { get; set; }
        public virtual String Entrepernas { get; set; }
        public virtual String Boca { get; set; }

        public virtual ProdutoBase ProdutoBase { get; set; }
        public virtual Barra Barra { get; set; }
        public virtual Comprimento Comprimento { get; set; }
    }
}