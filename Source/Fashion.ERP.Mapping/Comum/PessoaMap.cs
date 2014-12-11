using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public sealed class PessoaMap : FashionClassMap<Pessoa>
    {
        public PessoaMap()
            : base("pessoa", 50)
        {
            Map(x => x.TipoPessoa).Not.Nullable();
            Map(x => x.CpfCnpj).Length(18);
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.NomeFantasia).Length(100);
            Map(x => x.DocumentoIdentidade).Length(20);
            Map(x => x.OrgaoExpedidor).Length(20);
            Map(x => x.InscricaoEstadual).Length(20);
            Map(x => x.InscricaoMunicipal).Length(20);
            Map(x => x.InscricaoSuframa).Length(9);
            Map(x => x.DataNascimento);
            Map(x => x.Site).Length(100);
            Map(x => x.DataCadastro).Not.Nullable();

            References(x => x.Foto).Cascade.Delete();
            References(x => x.Fornecedor).Cascade.All();
            References(x => x.Transportadora).Cascade.All();
            References(x => x.Cliente).Cascade.All();
            References(x => x.PrestadorServico).Cascade.All();
            References(x => x.Unidade).Cascade.All();
            References(x => x.Funcionario).Cascade.All();
            References(x => x.Empresa).Cascade.All();

            HasMany(x => x.Enderecos)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.InformacaoBancarias)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.Contatos)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}