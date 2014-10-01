using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.Financeiro
{
    public class ChequeRecebido : DomainBase<Emitente>
    {
        private readonly IList<BaixaChequeRecebido> _baixaChequeRecebidos;
        private readonly IList<OcorrenciaCompensacao> _ocorrenciaCompensacoes;
        private readonly IList<Pessoa> _funcionarios;
        private readonly IList<Pessoa> _prestadorServicos;

        public ChequeRecebido()
        {
            _baixaChequeRecebidos = new List<BaixaChequeRecebido>();
            _ocorrenciaCompensacoes = new List<OcorrenciaCompensacao>();
            _funcionarios = new List<Pessoa>();
            _prestadorServicos = new List<Pessoa>();
        }

        public virtual int Comp { get; set; }
        public virtual string Agencia { get; set; }
        public virtual string Conta { get; set; }
        public virtual string NumeroCheque { get; set; }
        public virtual string Cmc7 { get; set; }
        public virtual double Valor { get; set; }
        public virtual string Nominal { get; set; }
        public virtual DateTime DataEmissao { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual DateTime? DataProrrogacao { get; set; }
        public virtual string Praca { get; set; }
        public virtual string Historico { get; set; }
        public virtual string Observacao { get; set; }
        public virtual double Saldo { get; set; }
        public virtual bool Compensado { get; set; }

        public virtual Pessoa Cliente { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual Emitente Emitente { get; set; }
        public virtual Unidade Unidade { get; set; }

        #region Funcionarios

        public virtual IReadOnlyCollection<Pessoa> Funcionarios
        {
            get { return new ReadOnlyCollection<Pessoa>(_funcionarios); }
        }

        public virtual void AddFuncionario(params Pessoa[] funcionarios)
        {
            foreach (var funcionario in funcionarios)
                if (!_funcionarios.Contains(funcionario))
                    _funcionarios.Add(funcionario);
        }

        public virtual void RemoveFuncionario(params Pessoa[] funcionarios)
        {
            foreach (var funcionario in funcionarios)
                if (_funcionarios.Contains(funcionario))
                    _funcionarios.Remove(funcionario);
        }

        #endregion

        #region PrestadorServicos

        public virtual IReadOnlyCollection<Pessoa> PrestadorServicos
        {
            get { return new ReadOnlyCollection<Pessoa>(_prestadorServicos); }
        }

        public virtual void AddPrestadorServico(params Pessoa[] prestadorServicos)
        {
            foreach (var prestadorServico in prestadorServicos)
                if (!_prestadorServicos.Contains(prestadorServico))
                    _prestadorServicos.Add(prestadorServico);
        }

        public virtual void RemovePrestadorServico(params Pessoa[] prestadorServicos)
        {
            foreach (var prestadorServico in prestadorServicos)
                if (_prestadorServicos.Contains(prestadorServico))
                    _prestadorServicos.Remove(prestadorServico);
        }

        #endregion

        #region BaixaChequeRecebidos

        public virtual IReadOnlyCollection<BaixaChequeRecebido> BaixaChequeRecebidos
        {
            get { return new ReadOnlyCollection<BaixaChequeRecebido>(_baixaChequeRecebidos); }
        }

        public virtual void AddBaixaChequeRecebido(params BaixaChequeRecebido[] baixaChequeRecebidos)
        {
            foreach (var baixaChequeRecebido in baixaChequeRecebidos)
            {
                if (!_baixaChequeRecebidos.Contains(baixaChequeRecebido))
                {
                    baixaChequeRecebido.ChequeRecebido = this;
                    _baixaChequeRecebidos.Add(baixaChequeRecebido);
                }
            }
        }

        public virtual void RemoveBaixaChequeRecebido(params BaixaChequeRecebido[] baixaChequeRecebidos)
        {
            foreach (var baixaChequeRecebido in baixaChequeRecebidos)
            {
                if (_baixaChequeRecebidos.Contains(baixaChequeRecebido))
                    _baixaChequeRecebidos.Remove(baixaChequeRecebido);
            }
        }

        #endregion

        #region OcorrenciaCompensacaos

        public virtual IReadOnlyCollection<OcorrenciaCompensacao> OcorrenciaCompensacoes
        {
            get { return new ReadOnlyCollection<OcorrenciaCompensacao>(_ocorrenciaCompensacoes); }
        }

        public virtual void AddOcorrenciaCompensacao(params OcorrenciaCompensacao[] ocorrenciaChequeRecebidos)
        {
            foreach (var ocorrenciaChequeRecebido in ocorrenciaChequeRecebidos)
            {
                if (!_ocorrenciaCompensacoes.Contains(ocorrenciaChequeRecebido))
                {
                    ocorrenciaChequeRecebido.ChequeRecebido = this;
                    _ocorrenciaCompensacoes.Add(ocorrenciaChequeRecebido);
                }
            }
        }

        public virtual void RemoveOcorrenciaCompensacao(params OcorrenciaCompensacao[] ocorrenciaChequeRecebidos)
        {
            foreach (var ocorrenciaChequeRecebido in ocorrenciaChequeRecebidos)
            {
                if (_ocorrenciaCompensacoes.Contains(ocorrenciaChequeRecebido))
                    _ocorrenciaCompensacoes.Remove(ocorrenciaChequeRecebido);
            }
        }

        #endregion
    }
}