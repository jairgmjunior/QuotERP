using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class BaixaChequeRecebido : DomainBase<BaixaChequeRecebido>
    {
        private readonly IList<RecebimentoChequeRecebido> _recebimentoChequeRecebidos;

        public BaixaChequeRecebido()
        {
            _recebimentoChequeRecebidos = new List<RecebimentoChequeRecebido>();            
        }

        public virtual DateTime Data { get; set; }
        public virtual double Despesa { get; set; }
        public virtual double ValorJuros { get; set; }
        public virtual double ValorDesconto { get; set; }
        public virtual double Valor { get; set; }
        public virtual string Historico { get; set; }
        public virtual string Observacao { get; set; }

        public virtual ChequeRecebido ChequeRecebido { get; set; }
        public virtual Pessoa Cobrador { get; set; }

        #region RecebimentoChequeRecebidos

        public virtual IReadOnlyCollection<RecebimentoChequeRecebido> RecebimentoChequeRecebidos
        {
            get { return new ReadOnlyCollection<RecebimentoChequeRecebido>(_recebimentoChequeRecebidos); }
        }

        public virtual void AddRecebimentoChequeRecebido(params RecebimentoChequeRecebido[] recebimentoChequeRecebidos)
        {
            foreach (var recebimentoChequeRecebido in recebimentoChequeRecebidos)
                if (!_recebimentoChequeRecebidos.Contains(recebimentoChequeRecebido))
                {
                    recebimentoChequeRecebido.BaixaChequeRecebido = this;
                    _recebimentoChequeRecebidos.Add(recebimentoChequeRecebido);
                }
        }

        public virtual void RemoveRecebimentoChequeRecebido(params RecebimentoChequeRecebido[] recebimentoChequeRecebidos)
        {
            foreach (var recebimentoChequeRecebido in recebimentoChequeRecebidos)
                if (_recebimentoChequeRecebidos.Contains(recebimentoChequeRecebido))
                    _recebimentoChequeRecebidos.Remove(recebimentoChequeRecebido);
        }

        #endregion
    }
}