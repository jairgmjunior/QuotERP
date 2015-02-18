using System;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaFoto : DomainEmpresaBase<FichaTecnicaFoto>
    {
        public virtual String Descricao { get; set; }
        public virtual Boolean Padrao { get; set; }
        public virtual Boolean Impressao { get; set; }
        public virtual Arquivo Arquivo { get; set; }
    }
}