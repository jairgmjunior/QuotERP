using Fashion.ERP.Domain.EngenhariaProduto;
namespace Fashion.ERP.Domain.Comum
{
    public class OperacaoProducao : DomainBase<OperacaoProducao>
    {
        public virtual string Descricao { get; set; }
        public virtual double Tempo { get; set; }
        public virtual double Custo { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual double PesoProdutividade { get; set; }

        public virtual SetorProducao SetorProducao { get; set; }
    }
}