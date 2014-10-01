namespace Fashion.ERP.Domain.Comum
{
    public class DepartamentoProducao : DomainBase<DepartamentoProducao>
    {
        public virtual string Nome { get; set; }
        public virtual bool Criacao { get; set; }
        public virtual bool Producao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}