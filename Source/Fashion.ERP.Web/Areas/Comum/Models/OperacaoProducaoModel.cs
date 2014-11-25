using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class OperacaoProducaoModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Descricao { get; set; }

        [Display(Name = "Tempo (segundos)")]
        [Required(ErrorMessage = "Informe o tempo em segundos")]
        public double Tempo { get; set; }

        [Required(ErrorMessage = "Informe o custo")]
        public double Custo { get; set; }

        [Display(Name = "Setor produtivo")]
        [Required(ErrorMessage = "Informe o setor produtivo")]
        public long? SetorProducao { get; set; }

        [Display(Name = "Departamento do setor produtivo")]
        [Required(ErrorMessage = "Informe o departamento do setor produtivo")]
        public long? DepartamentoProducao { get; set; }

        [Display(Name = "Peso Produtividade")]
        [Required(ErrorMessage = "Informe o peso da produtividade")]
        public double? PesoProdutividade { get; set; }

        [Display(Name = "Hierarquia")]
        public IList<TreeViewModel> TreeView { get; set; }

        #region Populate
        public void Populate(IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<SetorProducao> setorProducaoRepository,
            IRepository<OperacaoProducao> operacaoProducaoRepository)
        {
            TreeView = new List<TreeViewModel>();

            // Percorre todos os departamentos
            var departamentosProducao = departamentoProducaoRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            foreach (var departamentoProducao in departamentosProducao)
            {
                var departamentoProducaoScoped = departamentoProducao;

                var setoresProducao = setorProducaoRepository
                    .Find(p => p.DepartamentoProducao == departamentoProducaoScoped && p.Ativo)
                    .OrderBy(o => o.Nome)
                    .ToList();

                // Percorre todos os setores
                var setorProducaoTreeView = new List<TreeViewModel>();
                foreach (var setorProducao in setoresProducao)
                {
                    var setorProducaoScoped = setorProducao;

                    var operacoesProducao = operacaoProducaoRepository
                    .Find(p => p.SetorProducao == setorProducaoScoped && (p.Ativo || p.Id == Id))
                    .OrderBy(o => o.Descricao)
                    .ToList();

                    // Percorre todos as operações
                    var operacaoProducaoTreeView = new List<TreeViewModel>();
                    foreach (var operacaoProducao in operacoesProducao)
                    {
                        operacaoProducaoTreeView.Add(new TreeViewModel
                        {
                            Id = operacaoProducao.Id,
                            Name = operacaoProducao.Descricao,
                            IsChecked = operacaoProducao.Id == Id
                        });
                    }

                    setorProducaoTreeView.Add(new TreeViewModel
                    {
                        Id = setorProducao.Id,
                        Name = setorProducao.Nome,
                        Itens = operacaoProducaoTreeView
                    });
                }

                TreeView.Add(new TreeViewModel
                {
                    Id = departamentoProducao.Id,
                    Name = departamentoProducao.Nome,
                    Itens = setorProducaoTreeView
                });
            }
        }
        #endregion
    }
}