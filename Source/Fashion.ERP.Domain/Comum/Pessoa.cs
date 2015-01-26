using Fashion.ERP.Domain.Compras;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fashion.ERP.Domain.Comum
{
    public class Pessoa : DomainBase<Pessoa>
    {
        private readonly IList<Endereco> _enderecos;
        private readonly IList<InformacaoBancaria> _informacaoBancarias;
        private readonly IList<Contato> _contatos;

        public Pessoa()
        {
            _enderecos = new List<Endereco>();
            _informacaoBancarias = new List<InformacaoBancaria>();
            _contatos = new List<Contato>();
        }

        public virtual Arquivo Foto { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual PrestadorServico PrestadorServico { get; set; }
        public virtual Unidade Unidade { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Transportadora Transportadora { get; set; }

        public virtual TipoPessoa TipoPessoa { get; set; }
        public virtual string CpfCnpj { get; set; }
        public virtual string Nome { get; set; }
        public virtual string NomeFantasia { get; set; }
        public virtual string DocumentoIdentidade { get; set; }
        public virtual string OrgaoExpedidor { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual string InscricaoMunicipal { get; set; }
        public virtual string InscricaoSuframa { get; set; }
        public virtual DateTime? DataNascimento { get; set; }
        public virtual string Site { get; set; }
        public virtual DateTime DataCadastro { get; set; }

        public virtual Endereco EnderecoPadrao
        {
            //todo retornar TipoEndereco.Comercial se TipoPessoa.Juridico
            get
            {
                var enderecoResidencial = Enderecos.FirstOrDefault(x => x.TipoEndereco == TipoEndereco.Residencial);
                
                if (enderecoResidencial == null)
                {
                    return Enderecos.FirstOrDefault();
                }

                return enderecoResidencial;
            }
        }

        public virtual Contato ContatoPadrao
        {
            get
            {
                return _contatos.FirstOrDefault(contato =>
                {
                    if (TipoPessoa == TipoPessoa.Juridica && contato.TipoContato == TipoContato.Comercial)
                    {
                        return true;
                    } 

                    if (TipoPessoa == TipoPessoa.Fisica && contato.TipoContato == TipoContato.Residencial)
                    {
                        return true;
                    }

                    if (TipoPessoa == TipoPessoa.Exterior)
                    {
                        return true;
                    }

                    return false;
                });
            }
        }
        
        #region Enderecos

        public virtual IReadOnlyCollection<Endereco> Enderecos
        {
            get { return new ReadOnlyCollection<Endereco>(_enderecos); }
        }

        public virtual void AddEndereco(params Endereco[] enderecos)
        {
            foreach (var endereco in enderecos)
            {
                if (!_enderecos.Contains(endereco))
                {
                    endereco.Pessoa = this;
                    _enderecos.Add(endereco);
                }
            }
        }

        public virtual void RemoveEndereco(params Endereco[] enderecos)
        {
            foreach (var endereco in enderecos)
            {
                if (_enderecos.Contains(endereco))
                    _enderecos.Remove(endereco);
            }
        }

        #endregion

        #region InformacaoBancarias

        public virtual IReadOnlyCollection<InformacaoBancaria> InformacaoBancarias
        {
            get { return new ReadOnlyCollection<InformacaoBancaria>(_informacaoBancarias); }
        }

        public virtual void AddInformacaoBancaria(params InformacaoBancaria[] informacaoBancarias)
        {
            foreach (var informacaoBancaria in informacaoBancarias)
            {
                if (!_informacaoBancarias.Contains(informacaoBancaria))
                {
                    informacaoBancaria.Pessoa = this;
                    _informacaoBancarias.Add(informacaoBancaria);
                }
            }
        }

        public virtual void RemoveInformacaoBancaria(params InformacaoBancaria[] informacaoBancarias)
        {
            foreach (var informacaoBancaria in informacaoBancarias)
            {
                if (_informacaoBancarias.Contains(informacaoBancaria))
                    _informacaoBancarias.Remove(informacaoBancaria);
            }
        }

        #endregion

        #region Contatos

        public virtual IReadOnlyCollection<Contato> Contatos
        {
            get { return new ReadOnlyCollection<Contato>(_contatos); }
        }

        public virtual void AddContato(params Contato[] contatos)
        {
            foreach (var contato in contatos)
            {
                if (!_contatos.Contains(contato))
                {
                    contato.Pessoa = this;
                    _contatos.Add(contato);
                }
            }
        }

        public virtual void RemoveContato(params Contato[] contatos)
        {
            foreach (var contato in contatos)
            {
                if (_contatos.Contains(contato))
                    _contatos.Remove(contato);
            }
        }

        #endregion
    }
}