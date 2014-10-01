using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Helpers
{
    public static class PermissaoHelper
    {
        #region Variáveis
        const string PermissaoCacheSessionKey = "PermissaoCacheSessionKey";
        const string UsuarioPermissaoCacheSessionKey = "UsuarioPermissaoCacheSessionKey";
        const string PermissaoCacheApplicationKey = "PermissaoCacheApplicationKey";
        #endregion

        #region Propriedades
        private static ILogger Logger
        {
            get { return DependencyResolver.Current.GetService<ILogger>(); }
        }

        private static IRepository<Permissao> PermissaoRepository
        {
            get { return DependencyResolver.Current.GetService<IRepository<Permissao>>(); }
        }

        private static IRepository<Usuario> UsuarioRepository
        {
            get { return DependencyResolver.Current.GetService<IRepository<Usuario>>(); }
        }
        #endregion

        #region PossuiPermissao
        public static bool PossuiPermissao(string actionName, string controllerName, string areaName)
        {
            try
            {
                var permissoes = BuscaPermissoes(actionName, controllerName, areaName);

                // Se não existe permissão para esta url ou ela não exige permissão, retorna verdadeiro
                if (permissoes.Any() == false || permissoes.All(p => p.RequerPermissao == false))
                    return true;

                var permissoesUsuario = BuscaPermissoesUsuario();

                return permissoesUsuario.Any(p => permissoes.Any(q => q.Id == p.Id));
            }
            catch (Exception exception)
            {
                Logger.Error(exception.GetMessage());
            }
            
            return false;
        }
        #endregion

        #region BuscaUsuario
        public static Usuario BuscaUsuario()
        {
            var userId = FashionSecurity.GetLoggedUserId();

            if (!userId.HasValue)
                return null;

            return UsuarioRepository.Get(userId.Value);
        }
        #endregion

        #region BuscaPermissoesUsuario
        public static IList<Permissao> BuscaPermissoesUsuario()
        {
            var usuario = BuscaUsuario();
            
            if (usuario == null)
                return new List<Permissao>();

            // Verifica se é para remover o cache do usuário
            var usersRemoveCache = GetRemoveUserPermissionCache();
            if (usersRemoveCache.Contains(usuario.Login))
            {
                usersRemoveCache.Remove(usuario.Login);
                SetRemoveUserPermissionCache(usersRemoveCache);
                SetUserCache(null);
            }

            var cache = GetUserCache();

            if (cache.Any())
                return cache;

            cache = new List<Permissao>(usuario.Permissoes.Concat(usuario.PerfisDeAcesso.SelectMany(p => p.Permissoes)));
            SetUserCache(cache);

            return cache;
        }
        #endregion

        #region BuscaPermissoes
        private static IList<Permissao> BuscaPermissoes(string actionName, string controllerName, string areaName)
        {
            actionName = actionName.ToUpperInvariant();
            controllerName = controllerName.ToUpperInvariant();

            if (areaName != null)
                areaName = areaName.ToUpperInvariant();

            // Pesquisa no cache
            var queryCache = GetCache().Where(x => x.Action.ToUpper() == actionName && x.Controller.ToUpper() == controllerName);

            if (!string.IsNullOrWhiteSpace(areaName))
                queryCache = queryCache.Where(x => x.Area.ToUpper() == areaName);

            var permissoes = queryCache.ToList();

            if (permissoes.Any())
                return permissoes;

            // Se não encontrou no cache, pesquisa no banco
            var query = PermissaoRepository.Find()
                    .Where(x => x.Action.ToUpper() == actionName && x.Controller.ToUpper() == controllerName);

            if (!string.IsNullOrWhiteSpace(areaName))
                query = query.Where(x => x.Area.ToUpper() == areaName);

            permissoes = query.ToList();

            if (permissoes.Any())
                SaveInCache(permissoes);

            return permissoes;
        }
        #endregion

        #region GetCache
        private static IList<Permissao> GetCache()
        {
            return HttpContext.Current.Session[PermissaoCacheSessionKey] as IList<Permissao> ?? new List<Permissao>();
        }
        #endregion

        #region SaveInCache
        private static void SaveInCache(IEnumerable<Permissao> permissoes)
        {
            var cache = GetCache();

            foreach (var permissao in permissoes)
                if (!cache.Contains(permissao))
                    cache.Add(permissao);
            
            HttpContext.Current.Session[PermissaoCacheSessionKey] = cache;
        }
        #endregion

        #region GetUserCache
        private static IList<Permissao> GetUserCache()
        {
            return HttpContext.Current.Session[UsuarioPermissaoCacheSessionKey] as IList<Permissao> ?? new List<Permissao>();
        }
        #endregion

        #region SetUserCache
        private static void SetUserCache(IList<Permissao> cache)
        {
            HttpContext.Current.Session[UsuarioPermissaoCacheSessionKey] = cache;
        }
        #endregion

        #region GetRemoveUserPermissionCache
        /// <summary>
        /// Retorna uma lista de nome de usuários na qual o cache de permissões deve ser limpo.
        /// </summary>
        private static IList<string> GetRemoveUserPermissionCache()
        {
            return HttpContext.Current.Application[PermissaoCacheApplicationKey] as IList<string> ?? new List<string>();
        }
        #endregion

        #region SetRemoveUserPermissionCache
        /// <summary>
        /// Adiciona o login do usuário no qual o seu cache de permissões deve ser limpo.
        /// </summary>
        /// <param name="login">Login do usuário.</param>
        public static void SetRemoveUserPermissionCache(string login)
        {
            var cache = GetRemoveUserPermissionCache();

            cache.Add(login);
            HttpContext.Current.Application[PermissaoCacheApplicationKey] = cache;
        }

        private static void SetRemoveUserPermissionCache(IList<string> cache)
        {
            HttpContext.Current.Application[PermissaoCacheApplicationKey] = cache;
        }
        #endregion
    }
}