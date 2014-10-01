namespace Fashion.ERP.Domain.Comum
{
    public class Dependente : DomainBase<Dependente>
    {
        //public virtual Cliente Cliente { get; set; }

        public virtual string Nome { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Rg { get; set; }
        public virtual string OrgaoExpedidor { get; set; }

        public virtual GrauDependencia GrauDependencia { get; set; }
    }
}