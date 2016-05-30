using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProducaoProgramacaoMap : EmpresaClassMap<ProducaoProgramacao>
    {
        public ProducaoProgramacaoMap()
            : base("producaoprogramacao", 0)
        {
            Map(x => x.Data);
            Map(x => x.DataProgramada);
            Map(x => x.Observacao).Length(4000).Nullable();
            Map(x => x.Quantidade);

            References(x => x.Funcionario);
        }
    }
}