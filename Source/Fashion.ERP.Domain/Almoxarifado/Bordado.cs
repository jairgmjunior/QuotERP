namespace Fashion.ERP.Domain.Almoxarifado
{
    public class Bordado : DomainBase<Bordado>
    {
        public virtual string Descricao { get; set; }
        public virtual string Pontos { get; set; }
        public virtual string Aplicacao { get; set; }
        public virtual string Observacao { get; set; }
    }
}