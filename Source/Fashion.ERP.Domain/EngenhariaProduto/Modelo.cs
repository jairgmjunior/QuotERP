using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Domain.EngenhariaProduto
{
    public class Modelo : DomainBase<Modelo>
    {
        #region Variáveis
        private readonly IList<ModeloFoto> _fotos;
        private readonly IList<Variacao> _variacoes;
        private readonly IList<SequenciaProducao> _sequenciaProducoes;
        private readonly IList<string> _linhasTravete;
        private readonly IList<string> _linhasBordado;
        private readonly IList<string> _linhasPesponto;
        private readonly IList<ProgramacaoBordado> _programacaoBordados;
        #endregion

        #region Construtores
        public Modelo()
        {
            _fotos = new List<ModeloFoto>();
            _variacoes = new List<Variacao>();
            _sequenciaProducoes = new List<SequenciaProducao>();
            _linhasTravete = new List<string>();
            _linhasBordado = new List<string>();
            _linhasPesponto = new List<string>();
            _programacaoBordados = new List<ProgramacaoBordado>();
        }
        #endregion

        #region Propriedades
        public virtual string Referencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Tecido { get; set; }
        public virtual string Detalhamento { get; set; }
        public virtual string Complemento { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual bool? Aprovado { get; set; }
        public virtual DateTime? DataAprovacao { get; set; }
        public virtual string ObservacaoAprovacao { get; set; }
        public virtual string Observacao { get; set; }
        public virtual double? Cos { get; set; }
        public virtual double? Passante { get; set; }
        public virtual double? Entrepernas { get; set; }
        public virtual string Localizacao { get; set; }
        public virtual string TamanhoPadrao { get; set; }
        public virtual string LinhaCasa { get; set; }
        public virtual string Lavada { get; set; }
        public virtual double? Boca { get; set; }
        public virtual string Modelagem { get; set; }
        public virtual string EtiquetaMarca { get; set; }
        public virtual string EtiquetaComposicao { get; set; }
        public virtual string Tag { get; set; }
        public virtual DateTime? DataModelagem { get; set; }
        public virtual string TecidoComplementar { get; set; }
        public virtual string Forro { get; set; }
        public virtual string ZiperBraguilha { get; set; }
        public virtual string ZiperDetalhe { get; set; }
        public virtual string Dificuldade { get; set; }
        public virtual int? QuantidadeMix { get; set; }
        public virtual DateTime? DataRemessaProducao { get; set; }
        public virtual DateTime? DataPrevisaoEnvio { get; set; }
        public virtual String ChaveExterna { get; set; }

        public virtual int AnoAprovacao { get; set; }
        public virtual int NumeroAprovacao { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Colecao Colecao { get; set; }
        public virtual Classificacao Classificacao { get; set; }
        //public virtual ClassificacaoDificuldade ClassificacaoDificuldade { get; set; }
        //public virtual Material Material { get; set; }
        public virtual Segmento Segmento { get; set; }
        public virtual Natureza Natureza { get; set; }
        public virtual Barra Barra { get; set; }
        public virtual Comprimento Comprimento { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual ProdutoBase ProdutoBase { get; set; }
        public virtual Artigo Artigo { get; set; }
        public virtual FichaTecnica FichaTecnica { get; set; }
        public virtual Pessoa Estilista { get; set; }
        public virtual Pessoa Modelista { get; set; }
        public virtual Tamanho Tamanho { get; set; }
        #endregion

        #region LinhasTravete
        public virtual IReadOnlyCollection<string> LinhasTravete
        {
            get { return new ReadOnlyCollection<string>(_linhasTravete); }
        }

        public virtual void AddLinhaTravete(params string[] linhasTravete)
        {
            foreach (var linhaTravete in linhasTravete)
            {
                if (!_linhasTravete.Contains(linhaTravete))
                {
                    _linhasTravete.Add(linhaTravete);
                }
            }
        }

        public virtual void RemoveLinhaTravete(params string[] linhasTravete)
        {
            foreach (var linhaTravete in linhasTravete)
            {
                if (_linhasTravete.Contains(linhaTravete))
                    _linhasTravete.Remove(linhaTravete);
            }
        }

        public virtual void ClearLinhaTravete()
        {
            _linhasTravete.Clear();
        }

        #endregion

        #region LinhasBordado
        public virtual IReadOnlyCollection<string> LinhasBordado
        {
            get { return new ReadOnlyCollection<string>(_linhasBordado); }
        }

        public virtual void AddLinhaBordado(params string[] linhasBordado)
        {
            foreach (var linhaBordado in linhasBordado)
            {
                if (!_linhasBordado.Contains(linhaBordado))
                {
                    _linhasBordado.Add(linhaBordado);
                }
            }
        }

        public virtual void RemoveLinhaBordado(params string[] linhasBordado)
        {
            foreach (var linhaBordado in linhasBordado)
            {
                if (_linhasBordado.Contains(linhaBordado))
                    _linhasBordado.Remove(linhaBordado);
            }
        }

        public virtual void ClearLinhaBordado()
        {
            _linhasBordado.Clear();
        }

        #endregion

        #region LinhasPesponto
        public virtual IReadOnlyCollection<string> LinhasPesponto
        {
            get { return new ReadOnlyCollection<string>(_linhasPesponto); }
        }

        public virtual void AddLinhaPesponto(params string[] linhasPesponto)
        {
            foreach (var linhaPesponto in linhasPesponto)
            {
                if (!_linhasPesponto.Contains(linhaPesponto))
                {
                    _linhasPesponto.Add(linhaPesponto);
                }
            }
        }

        public virtual void RemoveLinhaPesponto(params string[] linhasPesponto)
        {
            foreach (var linhaPesponto in linhasPesponto)
            {
                if (_linhasPesponto.Contains(linhaPesponto))
                    _linhasPesponto.Remove(linhaPesponto);
            }
        }

        public virtual void ClearLinhaPesponto()
        {
            _linhasPesponto.Clear();
        }

        #endregion

        #region Fotos
        public virtual IReadOnlyCollection<ModeloFoto> Fotos
        {
            get { return new ReadOnlyCollection<ModeloFoto>(_fotos); }
        }

        public virtual void AddFoto(params ModeloFoto[] fotos)
        {
            foreach (var foto in fotos)
            {
                foto.Modelo = this;
                _fotos.Add(foto);
            }
        }

        public virtual void RemoveFoto(params ModeloFoto[] fotos)
        {
            foreach (var foto in fotos)
            {
                if (_fotos.Contains(foto))
                    _fotos.Remove(foto);
            }
        }

        public virtual void ClearFoto()
        {
            _fotos.Clear();
        }

        #endregion

        #region Variacoes

        public virtual IReadOnlyCollection<Variacao> Variacoes
        {
            get { return new ReadOnlyCollection<Variacao>(_variacoes); }
        }

        public virtual void AddVariacao(params Variacao[] variacoes)
        {
            foreach (var variacao in variacoes)
            {
                //variacao.Modelo = this;
                _variacoes.Add(variacao);
            }
        }

        public virtual void RemoveVariacao(params Variacao[] variacoes)
        {
            foreach (var variacao in variacoes)
            {
                if (_variacoes.Contains(variacao))
                    _variacoes.Remove(variacao);
            }
        }

        public virtual void ClearVariacao()
        {
            _variacoes.Clear();
        }

        #endregion

        #region SequenciaProducoes

        public virtual IReadOnlyCollection<SequenciaProducao> SequenciaProducoes
        {
            get { return new ReadOnlyCollection<SequenciaProducao>(_sequenciaProducoes); }
        }

        public virtual void AddFirstSequenciaProducao(params SequenciaProducao[] sequenciaProducoes)
        {
            Array.Reverse(sequenciaProducoes);

            SequenciaProducao[] novaSequenciaProducoes = new SequenciaProducao[_sequenciaProducoes.Count];
            _sequenciaProducoes.CopyTo(novaSequenciaProducoes, 0);
            _sequenciaProducoes.Clear();

            foreach (var sequenciaProducao in sequenciaProducoes)
            {
                _sequenciaProducoes.Add(sequenciaProducao);
            }

            foreach (var sequenciaProducao in novaSequenciaProducoes)
            {
                _sequenciaProducoes.Add(sequenciaProducao);
            }
        }

        public virtual void AddSequenciaProducao(params SequenciaProducao[] sequenciaProducoes)
        {
            foreach (var sequenciaProdutiva in sequenciaProducoes)
            {
                _sequenciaProducoes.Add(sequenciaProdutiva);
            }
        }

        public virtual void RemoveSequenciaProducao(params SequenciaProducao[] sequenciaProducoes)
        {
            foreach (var sequenciaProducao in sequenciaProducoes)
            {
                if (_sequenciaProducoes.Contains(sequenciaProducao))
                    _sequenciaProducoes.Remove(sequenciaProducao);
            }
        }

        public virtual void ClearSequenciaProducao()
        {
            _sequenciaProducoes.Clear();
        }

        #endregion

        #region ProgramacaoBordados

        public virtual IReadOnlyCollection<ProgramacaoBordado> ProgramacaoBordados
        {
            get { return new ReadOnlyCollection<ProgramacaoBordado>(_programacaoBordados); }
        }

        public virtual void AddProgramacaoBordado(params ProgramacaoBordado[] programacaoBordados)
        {
            foreach (var programacaoBordado in programacaoBordados)
            {
                //programacaoBordado.Modelo = this;
                _programacaoBordados.Add(programacaoBordado);
            }
        }

        public virtual void RemoveProgramacaoBordado(params ProgramacaoBordado[] programacaoBordados)
        {
            foreach (var programacaoBordado in programacaoBordados)
            {
                if (_programacaoBordados.Contains(programacaoBordado))
                    _programacaoBordados.Remove(programacaoBordado);
            }
        }

        #endregion

        public virtual void GereChaveExterna()
        {

            //concorrência
            String chave = "";

            var randNum = new Random();
            for (int i = 0; i < 8; i++)
            {
                var posicao = randNum.Next(26);
                var caracter = ObtenhaCaracter(posicao);
                chave += caracter;
            }

            ChaveExterna = chave;
        }

        protected virtual String ObtenhaCaracter(int num)
        {     
            String[] i = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "Y", "W", "X", "Z" };
            String c = i[num];
            return c;
        }
    }
}