using System.Collections;
using System.Collections.Generic;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Natureza : DomainBase<Natureza>
    {
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }

        public virtual IList<SequenciaOperacional> SequenciasOperacionais { get; set; }

        #region Construtores

        public Natureza()
        {
                SequenciasOperacionais = new List<SequenciaOperacional>();
        }

        #endregion

        #region SequenciasOperacionais
        public virtual void AddSequenciaOperacao(params SequenciaOperacional[] sequenciasOperacionais)
        {
            foreach (var sequenciaOperacional in sequenciasOperacionais)
            {
                SequenciasOperacionais.Add(sequenciaOperacional);
            }
        }

        #endregion
    }
}