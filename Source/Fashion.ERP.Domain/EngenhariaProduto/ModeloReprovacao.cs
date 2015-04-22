using System;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloReprovacao : DomainEmpresaBase<ModeloReprovacao>
    {
         public virtual String Motivo { get; set; }
    }
}