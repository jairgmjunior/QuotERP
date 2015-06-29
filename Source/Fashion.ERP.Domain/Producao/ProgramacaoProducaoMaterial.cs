using Fashion.ERP.Domain.Almoxarifado;

namespace Fashion.ERP.Domain.Producao
{
    public class ProgramacaoProducaoMaterial : DomainEmpresaBase<ProgramacaoProducaoMaterial>
    {
        public virtual double Quantidade { get; set; }
        public virtual ReservaMaterial ReservaMaterial { get; set; }
        public virtual Material Material { get; set; }
    }
}