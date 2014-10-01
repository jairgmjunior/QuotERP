namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class OperacaoProducao : DomainBase<OperacaoProducao>
    {
        public virtual string Descricao { get; set; }
        public virtual double Tempo { get; set; }
        public virtual double Custo { get; set; }
        public virtual bool Ativo { get; set; }

        public virtual SetorProducao SetorProducao { get; set; }
    }
}