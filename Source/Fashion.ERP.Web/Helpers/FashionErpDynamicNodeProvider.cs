using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Comum;
using MvcSiteMapProvider;

namespace Fashion.ERP.Web.Helpers
{
    public class FashionErpDynamicNodeProvider : DynamicNodeProviderBase
    {
        #region GetDynamicNodeCollection
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode siteMapNode)
        {
            var permissoes = PermissaoHelper.BuscaPermissoesUsuario();
            var ordered = Order(permissoes);

            return ordered.Select(GetNode).ToList();
        }
        #endregion

        #region GetNode
        public DynamicNode GetNode(Permissao permissao)
        {
            var node = new DynamicNode
            {
                Key = permissao.Id.ToString(),
                Title = permissao.Descricao,
                Description = permissao.Descricao,
                Action = permissao.Action,
                Controller = permissao.Controller,
                Area = permissao.Area,
                ParentKey = permissao.PermissaoPai != null ? permissao.PermissaoPai.Id.ToString() : null,
                Order = permissao.Ordem,
                Clickable = !(string.IsNullOrEmpty(permissao.Action))
            };

            if (!permissao.ExibeNoMenu)
                node.Attributes.Add("show", "false");

            return node;
        }
        #endregion

        #region Order
        IEnumerable<Permissao> Order(IList<Permissao> nodes)
        {
            var roots = nodes.Where(node => node.PermissaoPai == null).OrderBy(p => p.Descricao);

            var permissoes = new List<Permissao>();
            foreach (var root in roots)
            {
                permissoes.AddRange(Order(root, nodes));
            }

            return permissoes;
        }

        IEnumerable<Permissao> Order(Permissao root, IList<Permissao> nodes)
        {
            yield return root;

            foreach (var child in nodes.Where(n => n.PermissaoPai == root).OrderBy(p => p.Descricao))
                foreach (var node in Order(child, nodes))
                    yield return node;
        }
        #endregion
    }
}