using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Models
{
    public class SubcategoriaModel : IModel
    {
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(60, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe a categoria")]
        public long Categoria { get; set; }

        [Display(Name = "Hierarquia")]
        public IList<TreeViewModel> TreeView { get; set; }

        #region Populate
        public void Populate(IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Categoria> categoriaRepository)
        {
            TreeView = new List<TreeViewModel>();

            // Percorre todos as categorias
            var categorias = categoriaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            foreach (var categoria in categorias)
            {
                var categoriaScoped = categoria;
                var subcategorias = subcategoriaRepository
                    .Find(p => p.Categoria == categoriaScoped && (p.Ativo || p.Id == Id))
                    .OrderBy(o => o.Nome)
                    .ToList();

                var subcategoriaTreeView = new List<TreeViewModel>();
                
                // Percorre todos as subcategorias
                foreach (var subcategoria in subcategorias)
                {
                    subcategoriaTreeView.Add(new TreeViewModel
                    {
                        Id = subcategoria.Id,
                        Name = subcategoria.Nome,
                        IsChecked = subcategoria.Id == Id
                    });
                }

                TreeView.Add(new TreeViewModel
                {
                    Id = categoria.Id,
                    Name = categoria.Nome,
                    Itens = subcategoriaTreeView
                });
            }
        }
        #endregion
    }
}