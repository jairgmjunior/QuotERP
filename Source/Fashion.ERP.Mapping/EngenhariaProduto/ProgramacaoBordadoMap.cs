using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ProgramacaoBordadoMap : FashionClassMap<ProgramacaoBordado>
    {
        public ProgramacaoBordadoMap()
            : base("programacaobordado", 0)
        {
            Map(x => x.Descricao).Length(100).Not.Nullable();
            Map(x => x.NomeArquivo).Length(100);
            Map(x => x.Data).Not.Nullable();
            Map(x => x.QuantidadePontos).Not.Nullable();
            Map(x => x.QuantidadeCores).Not.Nullable();
            Map(x => x.Aplicacao).Length(250);
            Map(x => x.Observacao).Length(250);

            References(x => x.Arquivo).Cascade.Delete();
            References(x => x.ProgramadorBordado).Not.Nullable();
        }
    }
}