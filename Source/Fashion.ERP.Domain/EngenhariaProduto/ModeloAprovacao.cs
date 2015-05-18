using System;
using Fashion.ERP.Domain.Producao;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class ModeloAprovacao : DomainEmpresaBase<ModeloAprovacao>
    {
        public virtual String Observacao { get; set; }
        public virtual long Quantidade { get; set; }
        public virtual String Referencia { get; set; }
        public virtual String Descricao { get; set; }
        public virtual long? MedidaBarra { get; set; }
        public virtual long? MedidaComprimento { get; set; }
        public virtual Comprimento Comprimento { get; set; }
        public virtual Barra Barra { get; set; }
        public virtual ProdutoBase ProdutoBase { get; set; }
        public virtual FichaTecnica FichaTecnica { get; set; }
        public virtual ModeloAprovacaoMatrizCorte ModeloAprovacaoMatrizCorte { get; set; }
    }
}