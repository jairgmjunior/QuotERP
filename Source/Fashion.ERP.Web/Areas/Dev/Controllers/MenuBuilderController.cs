using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Areas.Dev.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork.Logger;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Dev.Controllers
{
    public partial class MenuBuilderController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Permissao> _permissaoRepository;
        private readonly ILogger _logger;
        private List<string> _nhLogger = new List<string>();
        #endregion

        #region Construtores
        public MenuBuilderController(ILogger logger, IRepository<Permissao> permissaoRepository)
        {
            _permissaoRepository = permissaoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Index(long[] id, string[] text, long[] parentId,
            string[] area, string[] controller, string[] action, bool[] exibeNoMenu)
        {
            try
            {
                // Verifica se a permissão foi removida
                var permissoes = _permissaoRepository.Find(p => p.PermissaoPai == null).ToList();
                var listPermissoes = Flatten(permissoes).ToList();
                foreach (var permissao in listPermissoes)
                {
                    if (!id.Contains(permissao.Id.GetValueOrDefault()))
                        _permissaoRepository.Delete(permissao);
                }

                NhLoggerEvents.NhLogger += NhLoggerEventsOnNhLogger;

                // Verificar o que mudou e alterar no BD
                for (int i = 0; i < id.Length; i++)
                {

                    if (id[i] < 0) // Não existe no BD
                    {
                        var permissao = new Permissao
                        {
                            Descricao = text[i],
                            ExibeNoMenu = exibeNoMenu[i],
                            Action = VerificaNull(action[i]),
                            Controller = VerificaNull(controller[i]),
                            Area = VerificaNull(area[i]),
                            PermissaoPai = parentId[i] > 0 ? _permissaoRepository.Load(parentId[i]) : null
                        };
                        permissao.RequerPermissao = permissao.Action != null;
                        permissao = _permissaoRepository.Merge(permissao);
                        //_permissaoRepository.Evict(permissao); // Remove do cache para não ocorrer problemas

                        // Atualizar o parentId do novo item
                        for (int j = 0; j < id.Length; j++)
                        {
                            if (parentId[j] == id[i])
                            {
                                parentId[j] = permissao.Id.GetValueOrDefault();
                            }
                        }
                    }
                    else
                    {
                        var permissao = _permissaoRepository.Get(id[i]);
                        permissao.Descricao = text[i];
                        permissao.ExibeNoMenu = exibeNoMenu[i];
                        permissao.Action = VerificaNull(action[i]);
                        permissao.Controller = VerificaNull(controller[i]);
                        permissao.Area = VerificaNull(area[i]);
                        permissao.PermissaoPai = parentId[i] > 0 ? _permissaoRepository.Load(parentId[i]) : null;

                        _permissaoRepository.Update(permissao);
                        _permissaoRepository.Evict(permissao); // Remove do cache para não ocorrer problemas
                    }
                }

                Framework.UnitOfWork.Session.Current.Transaction.Commit();
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.GetMessage());
            }
            finally
            {
                NhLoggerEvents.NhLogger -= NhLoggerEventsOnNhLogger;
                ViewBag.Sql = string.Join("\n", _nhLogger.Select(p => p));
            }

            return View();
        }

        private void NhLoggerEventsOnNhLogger(NhLoggerEventArgs nhLoggerEventArgs)
        {
            _nhLogger.Add(nhLoggerEventArgs.Mensagem);
        }

        #endregion

        #region LerItensMenu
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerItensMenu()
        {
            var permissoes = _permissaoRepository.Find(p => p.PermissaoPai == null).ToList();
            var nodes = ListarItensMenu(permissoes);

            if (!nodes.Any())
            {
                nodes.Add(new Node{text = "Não existem permissões cadastradas."});
            }

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LerItensDisponiveis
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerItensDisponiveis()
        {
            // Remover os itens existentes no menu
            var permissoes = _permissaoRepository.Find(p => p.PermissaoPai == null).ToList();
            var listPermissoes = Flatten(permissoes).ToList();

            var nodes = ListaItensDisponiveis(listPermissoes);

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Métodos

        #region ListarItensMenu
        private IList<Node> ListarItensMenu(IEnumerable<Permissao> permissoes)
        {
            var nodes = new List<Node>();

            foreach (var permissao in permissoes)
            {
                nodes.Add(new Node
                {
                    id = permissao.Id.GetValueOrDefault(),
                    parentid = permissao.PermissaoPai != null ? permissao.PermissaoPai.Id.GetValueOrDefault() : 0,
                    text = permissao.Descricao,
                    area = permissao.Area,
                    controller = permissao.Controller,
                    action = permissao.Action,
                    exibeNoMenu = permissao.ExibeNoMenu,
                    items = ListarItensMenu(permissao.PermissoesFilhas)
                });
            }

            return nodes;
        }
        #endregion

        #region ListaItensDisponiveis
        private static List<Node> ListaItensDisponiveis(IList<Permissao> permissoes)
        {
            var itens = BuscarItensDisponiveis();
            
            var nodes = new List<Node>();
            var id = -1;

            var areas = itens.Select(p => p.Item1).Distinct();
            foreach (var a in areas)
            {
                string area = a;

                var areaNode = new Node
                {
                    id = id--, 
                    text = area, 
                    area = area,
                    exibeNoMenu = true
                };

                var controllers = itens.Where(p => p.Item1 == area).Select(p => p.Item2).Distinct();
                foreach (var c in controllers)
                {
                    string controller = c;

                    var controllerNode = new Node
                    {
                        id = id--, 
                        text = controller, 
                        parentid = areaNode.id, 
                        area = area, 
                        controller = controller,
                        action = "Index",
                        exibeNoMenu = true
                    };

                    var actions = itens.Where(p => p.Item2 == controller).Select(p => p.Item3).Distinct();
                    foreach (var action in actions)
                    {
                        if (permissoes.Any(p => p.Area == area && p.Controller == controller && p.Action == action) || action == "Index")
                            continue;

                        var actionNode = new Node
                        {
                            id = id--, 
                            text = action, 
                            parentid = controllerNode.id,
                            area = area,
                            controller = controller,
                            action = action,
                            exibeNoMenu = false
                        };

                        if (controllerNode.items == null)
                            controllerNode.items = new List<Node>();

                        controllerNode.items.Add(actionNode);
                    }

                    // Se não houver nenhuma action, não adicionar à area
                    if (controllerNode.items == null || !controllerNode.items.Any())
                        continue;

                    if (areaNode.items == null)
                        areaNode.items = new List<Node>();

                    areaNode.items.Add(controllerNode);
                }

                // Se não houver nenhum controller, não adicionar à raiz
                if (areaNode.items == null || !areaNode.items.Any())
                    continue;

                nodes.Add(areaNode);
            }

            return nodes;
        }
        #endregion

        #region Flatten
        static IEnumerable<Permissao> Flatten(IEnumerable<Permissao> nodes)
        {
            foreach (var node in nodes)
            {
                yield return node;

                if (node.PermissoesFilhas != null)
                    foreach (var item in Flatten(node.PermissoesFilhas))
                        yield return item;
            }
        }
        #endregion

        #region BuscarItensDisponiveis
        /// <summary>
        /// Retorna uma lista de tuplas [area, controller, action]
        /// </summary>
        private static List<Tuple<string, string, string>> BuscarItensDisponiveis()
        {
            var itens = new List<Tuple<string, string, string>>();

            foreach (var subClass in BuscaSubClasses<BaseController>())
            {
                var descriptor = new ReflectedControllerDescriptor(subClass);

                if (descriptor.IsDefined(typeof(AllowAnonymousAttribute), false) ||
                    descriptor.IsDefined(typeof(NonActionAttribute), false))
                    continue;

                var controllerName = descriptor.ControllerName;

                if (controllerName.StartsWith("T4MVC"))
                    continue;

                var areaName = NomeArea(subClass.Namespace);

                foreach (var action in descriptor.GetCanonicalActions())
                {
                    if (action.IsDefined(typeof(AllowAnonymousAttribute), false) ||
                        action.IsDefined(typeof(NonActionAttribute), false) ||
                        action.IsDefined(typeof(HttpPostAttribute), false) ||
                        action.IsDefined(typeof(AjaxOnlyAttribute), false) ||
                        action.IsDefined(typeof(ChildActionOnlyAttribute), false))
                        continue;

                    var actionName = action.ActionName;
                    itens.Add(new Tuple<string, string, string>(areaName, controllerName, actionName));
                }
            }

            return itens;
        }
        #endregion

        #region NomeArea
        private static string NomeArea(string @namespace)
        {
            const string areaString = "Areas";

            var areaIndex = @namespace.IndexOf(areaString, StringComparison.InvariantCultureIgnoreCase);
            if (areaIndex < 0)
                return null;

            var list = @namespace.Split('.');
            return list[list.Length - 2];
        }
        #endregion

        #region BuscaSubClasses
        private static List<Type> BuscaSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }
        #endregion

        #region VerificaNull
        private static string VerificaNull(string valor)
        {
            return valor == "null" || valor == "undefined" ? null : valor;
        }
        #endregion

        #endregion
    }
}