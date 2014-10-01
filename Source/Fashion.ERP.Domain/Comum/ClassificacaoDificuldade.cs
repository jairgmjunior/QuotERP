namespace Fashion.ERP.Domain.Comum
{
    public class ClassificacaoDificuldade : DomainBase<ClassificacaoDificuldade>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Criacao { get; set; }
        public virtual bool Producao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}