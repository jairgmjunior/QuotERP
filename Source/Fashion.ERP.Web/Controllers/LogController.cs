using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Web.Controllers
{
    public partial class LogController : BaseController
    {
        #region Variáveis
        private readonly IRepository<Usuario> _usuarioRepository;
        #endregion

        #region Construtores
        public LogController(IRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            if (TemPermissao() == false)
                return HttpNotFound();

            var list = new List<GridLogModel>
            { 
                new GridLogModel{ LogName = "Error"},
                new GridLogModel{ LogName = "Fatal"},
                new GridLogModel{ LogName = "Info"},
                new GridLogModel{ LogName = "Trace"}
            };

            return View(list);
        }
        #endregion

        #region Visualizar
        public virtual async Task<ActionResult> Visualizar(string logName)
        {
            if (TemPermissao() == false)
                return HttpNotFound();

            var model = new List<LogModel>();

            var filename = Server.MapPath("~/" + logName + ".log");

            try
            {
                using (TextReader file = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    string line, record = string.Empty;

                    while ((line = await file.ReadLineAsync()) != null)
                    {
                        if (line.StartsWith("!"))
                            record = line.Substring(1);
                        else
                            record += Environment.NewLine + line;

                        if (line.EndsWith("!"))
                            model.Add(ParseLine(record.Substring(0, record.Length - 1)));
                    }
                }
            }
            catch (Exception exception)
            {
                var ip = Request.ServerVariables["remote_addr"];
                model.Add(new LogModel { Classe = "LogController", Data = DateTime.Now, IpAddress = ip, Mensagem = exception.Message, Usuario = User.Identity.Name });
            }

            // Inverte a ordem
            model.Reverse();

            return View(model);
        }
        #endregion

        #region ParseLine
        private LogModel ParseLine(string line)
        {
            var model = new LogModel();

            var cols = line.Split('|');

            DateTime dateTime;
            if (DateTime.TryParse(cols[0].Trim(), out dateTime))
                model.Data = dateTime;

            model.Classe = cols[1];
            model.Usuario = cols[2];
            model.IpAddress = cols[3];
            model.Mensagem = cols[4];
            //model.Mensagem = HttpUtility.HtmlEncode(cols[3]);

            return model;
        }
        #endregion

        #region Limpar
        public virtual ActionResult Limpar(string logName)
        {
            if (TemPermissao() == false)
                return HttpNotFound();

            return RedirectToAction("Index");
        }
        #endregion
        
        #endregion

        #region Métodos

        #region TemPermissao
        private bool TemPermissao()
        {
            return true;

            var userId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(userId);
            return usuario != null && usuario.Administrador;
        }
        #endregion

        #endregion
    }
}