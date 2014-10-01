using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class DepositoMaterialModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe um nome")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Data abertura")]
        [Required(ErrorMessage = "Informe a data de abertura")]
        public DateTime DataAbertura { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Informe a unidade")]
        public long? Unidade { get; set; }

        public IList<long> Funcionarios { get; set; }

        [Display(Name = "Hierarquia")]
        public IList<TreeViewModel> TreeView { get; set; }

        #region Populate
        public void Populate(IRepository<Pessoa> pessoaRepository, IRepository<DepositoMaterial> depositoMaterialRepository)
        {
            TreeView = new List<TreeViewModel>();

            // Percorre todos as unidades
            var unidades = pessoaRepository.Find(p => p.Unidade.Ativo).OrderBy(o => o.Nome).ToList();
            foreach (var unidade in unidades)
            {
                var unidadeScoped = unidade;

                var depositos = depositoMaterialRepository
                    .Find(p => p.Unidade == unidadeScoped && p.Ativo)
                    .OrderBy(o => o.Nome)
                    .ToList();

                // Percorre todos os depósitos
                var depositoTreeView = new List<TreeViewModel>();
                foreach (var deposito in depositos)
                {
                    var funcionarios = deposito.Funcionarios;

                    // Percorre todos os funcionários
                    var funcionarioTreeView = new List<TreeViewModel>();
                    foreach (var funcionario in funcionarios)
                    {
                        funcionarioTreeView.Add(new TreeViewModel
                        {
                            Id = funcionario.Id,
                            Name = funcionario.Nome,
                            IsChecked = funcionario.Id == Id
                        });
                    }

                    depositoTreeView.Add(new TreeViewModel
                    {
                        Id = deposito.Id,
                        Name = deposito.Nome,
                        Itens = funcionarioTreeView
                    });
                }

                TreeView.Add(new TreeViewModel
                {
                    Id = unidade.Id,
                    Name = unidade.Nome,
                    Itens = depositoTreeView
                });
            }
        }
        #endregion
    }
}