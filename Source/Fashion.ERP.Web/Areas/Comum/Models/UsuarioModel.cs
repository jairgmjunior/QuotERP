using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Web.Areas.Comum.Models
{
    public class UsuarioModel : IModel
    {
        public UsuarioModel()
        {
            Permissoes = new long?[0];
            PerfisDeAcesso = new long?[0];
        }

        [Key]
        public long? Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, ErrorMessage = "{0} não deve ser maior que {1} caracteres")]
        public string Login { get; set; }

        [Display(Name = "Permissões")]
        public long?[] PerfisDeAcesso { get; set; }

        public MultiSelectList PerfisDeAcessoList { get; set; }

        [Display(Name = "Permissões Específicas")]
        public long?[] Permissoes { get; set; }

        public IList<TreeViewModel> PermissoesList { get; set; }

        #region Populate
        public void Populate()
        {
            PermissoesList = new List<TreeViewModel>();

            var permissoes = RepositoryFactory.Create<Permissao>()
                .Find(x => x.PermissaoPai == null && (x.ExibeNoMenu || x.RequerPermissao)).ToList();

            AddTreeViewItem(null, permissoes);

            var perfisDeAcesso = RepositoryFactory.Create<PerfilDeAcesso>()
                .Find()
                .Select(x => new PerfilDeAcesso { Id = x.Id, Nome = x.Nome })
                .ToList();

            PerfisDeAcessoList = new MultiSelectList(perfisDeAcesso, "Id", "Nome", PerfisDeAcesso);
        }
        #endregion

        #region AddTreeViewItem
        public void AddTreeViewItem(TreeViewModel pai, IList<Permissao> permissoes)
        {
            foreach (var permissao in permissoes)
            {
                var item = new TreeViewModel
                {
                    Id = permissao.Id,
                    Name = permissao.Descricao
                };

                if (Permissoes != null)
                    item.IsChecked = Permissoes.Any(x => x == permissao.Id);

                if (permissao.PermissoesFilhas != null)
                    AddTreeViewItem(item, permissao.PermissoesFilhas.Where(x => x.ExibeNoMenu || x.RequerPermissao).ToList());

                if (pai == null)
                    PermissoesList.Add(item);
                else
                    pai.Itens.Add(item);
            }
        }
        #endregion
    }
}