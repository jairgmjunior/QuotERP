namespace Fashion.ERP.Domain.Comum
{
    public class Endereco : DomainBase<Endereco>
    {
        public virtual TipoEndereco TipoEndereco { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cep { get; set; }

        public virtual string Resumo
        {
            get { return Logradouro + " N.º" + Numero + " " + Complemento + " " + Bairro; }
        }

        public virtual Pessoa Pessoa { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}