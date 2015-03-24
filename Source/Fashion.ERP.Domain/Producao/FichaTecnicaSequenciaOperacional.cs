using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Producao
{
    public class FichaTecnicaSequenciaOperacional: DomainEmpresaBase<FichaTecnicaSequenciaOperacional>
    {
        public virtual SetorProducao SetorProducao { get; set; }
        public virtual DepartamentoProducao DepartamentoProducao { get; set; }
        public virtual OperacaoProducao OperacaoProducao { get; set; }

        public virtual double Custo { get; set; }
        public virtual double Tempo { get; set; }
        public virtual double PesoProdutividade { get; set; }
    }
}


