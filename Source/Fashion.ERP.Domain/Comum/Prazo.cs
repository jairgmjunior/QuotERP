namespace Fashion.ERP.Domain.Comum
{
    public class Prazo : DomainBase<Prazo>
    {
        public virtual string Descricao { get; set; }
        public virtual bool AVista { get; set; }
        //[Range(1, 24, ErrorMessage = "A quantidade de parcelas deve ser entre {1} e {2}.")]
        public virtual int QuantidadeParcelas { get; set; }
        public virtual int PrazoPrimeiraParcela { get; set; }
        public virtual int Intervalo { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual bool Padrao { get; set; }
    }
}