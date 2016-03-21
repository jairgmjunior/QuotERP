using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public HomeController(ILogger logger, IRepository<Usuario> usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }
        #endregion

        #region Views
        
        #region Index
        public virtual ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            var model = new LoginModel();

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                model.ReturnUrl = returnUrl;
            }

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string decodedUrl = "";
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                        decodedUrl = Server.UrlDecode(model.ReturnUrl);

                    var usuario = _usuarioRepository.Find(x => x.Login == model.Usuario).FirstOrDefault();

                    if (usuario != null && usuario.Autenticar(model.Senha))
                    {
                        FashionSecurity.Login(usuario.Id, usuario.Nome, 10080 /* 1 semana */, model.PermanecerLogado);
                        
                        if (Url.IsLocalUrl(decodedUrl))
                        {
                            return Redirect(decodedUrl);
                        }
                        
                        return RedirectToAction("Index", "Home");
                    }

                    this.AddErrorMessage("O nome de usuário ou a senha inserido está incorreto.");
                }
                catch (Exception exception)
                {
                    var message = exception.GetMessage();
                    _logger.Error(message);
                    this.AddErrorMessage(message);
                }
            }

            return PartialView(model);
        }
        #endregion

        #region Logout
        [AllowAnonymous]
        public virtual ActionResult Logout()
        {
            FashionSecurity.Logout();

            this.AddSuccessMessage("Logout efetuado com sucesso.");
            return RedirectToAction("Login");
        }
        #endregion

        #endregion
    }
}
