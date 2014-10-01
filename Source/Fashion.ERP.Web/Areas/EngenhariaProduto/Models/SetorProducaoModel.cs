using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Models
{
    public class SetorProducaoModel : IModel
    {
        public long? Id { get; set; }
        
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(100, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Departamento produtivo")]
        [Required(ErrorMessage = "Informe o departamento produtivo")]
        public long? DepartamentoProducao { get; set; }

        [Display(Name = "Hierarquia")]
        public IList<TreeViewModel> TreeView { get; set; }

        #region Populate
        public void Populate(IRepository<DepartamentoProducao> departamentoProducaoRepository, 
            IRepository<SetorProducao> setorProducaoRepository)
        {
            TreeView = new List<TreeViewModel>();

            var departamentosProducao = departamentoProducaoRepository.Find(p => p.Ativo).ToList();

            foreach (var departamentoProducao in departamentosProducao)
            {
                var departamentoProducaoScoped = departamentoProducao;
                var setoresProducao = setorProducaoRepository
                    .Find(p => p.DepartamentoProducao == departamentoProducaoScoped && (p.Ativo || p.Id == Id))
                    .ToList();

                var setorProducaoTreeView = new List<TreeViewModel>();
                foreach (var setorProducao in setoresProducao)
                {
                    setorProducaoTreeView.Add(new TreeViewModel
                    {
                        Id = setorProducao.Id,
                        Name = setorProducao.Nome,
                        IsChecked = setorProducao.Id == Id
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