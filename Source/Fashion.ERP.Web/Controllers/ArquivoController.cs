using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using Fashion.ERP.Domain;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Controllers
{
    public partial class ArquivoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Arquivo> _arquivoRepository;
        private readonly ILogger _logger;

        #endregion

        #region Construtores
        public ArquivoController(ILogger logger, IRepository<Arquivo> arquivoRepository)
        {
            _arquivoRepository = arquivoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region UploadArquivoTemp
        [HttpPost, AjaxOnly]
        public virtual JsonResult UploadArquivoTemp()
        {
            try
            {
                var file = Request.Files[0];

                if (file != null)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var filename = Upload.GenerateTempFilename(fileExtension).GetTempFilePath();

                    file.SaveAs(filename);

                    return Json(new { url = ConcatRandomQueryString(filename.GetTempFileUrl()), nome = Path.GetFileName(filename) });
                }
            }
            catch (Exception exception)
            {
                _logger.Info(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao enviar o arquivo para o servidor." });
        }
        #endregion
        
        #region Download
		public virtual ActionResult Download(long id)
        {
            var arquivo = _arquivoRepository.Get(id);

            if (arquivo != null)
                return File(arquivo.Nome.GetFilePath(), "application/octet-stream", arquivo.Nome);

            return new HttpStatusCodeResult(404);
        }
	    #endregion

        #region UploadFotoTemp
        [HttpPost, AjaxOnly]
        public virtual JsonResult UploadFotoTemp()
        {
            try
            {
                var file = Request.Files[0];

                if (file != null)
                {
                    var filename = Upload.GenerateTempFilename("jpeg").GetTempFilePath();

                    Image.FromStream(file.InputStream, true, true).SaveJpeg(filename, 60);

                    return Json(new { url = ConcatRandomQueryString(filename.GetTempFileUrl()), fotoNome = Path.GetFileName(filename) });
                }
            }
            catch (Exception exception)
            {
                _logger.Info(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao enviar a imagem para o servidor." });
        }
        #endregion

        #region RecortarImagem
        [HttpPost, AjaxOnly]
        public virtual JsonResult RecortarImagem(string urlTemp, int x, int y, int w, int h)
        {
            try
            {
                // Remove qualquer querystring da url
                urlTemp = urlTemp.Split('?')[0];

                var filename = Path.GetFileName(urlTemp);
                var pathTemp = filename.GetTempFilePath();

                var source = Image.FromFile(pathTemp);
                var format = source.RawFormat;
                var target = source.Crop(w, h, x, y).CreateThumbnail(150, 150);

                using (var fileStream = System.IO.File.OpenWrite(pathTemp))
                {
                    target.Save(fileStream, format);
                }

                return Json(new { url = ConcatRandomQueryString(urlTemp), fotoNome = Path.GetFileName(pathTemp) });
            }
            catch (Exception exception)
            {
                _logger.Info(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao salvar a imagem no servidor." });
        }
        #endregion

        #region Imagens
        public virtual ActionResult Imagens(long id)
        {
            var arquivo = _arquivoRepository.Get(id);

            if (arquivo != null && System.IO.File.Exists(arquivo.Nome.GetFilePath()))
                return File(arquivo.Nome.GetFilePath());

            return new HttpStatusCodeResult(404);
        }
        #endregion

        #region Excluir
        //[HttpPost, AjaxOnly]
        //public virtual ActionResult Excluir(long id)
        //{
        //    try
        //    {
        //        var arquivo = _arquivoRepository.Get(id);

        //        if (arquivo != null)
        //            _arquivoRepository.Delete(arquivo);

        //        return Json(new { result = "Excluído com sucesso." });
        //    }
        //    catch (System.Exception exception)
        //    {
        //        _logger.Info(exception.GetMessage());
        //    }

        //    return Json(new { Error = "Ocorreu um erro ao excluir a imagem." });
        //}
        #endregion

        #region SalvarArquivo
        [NonAction]
        public static Arquivo SalvarArquivo(string arquivoNome, string titulo = "")
        {
            var tempPath = arquivoNome.GetTempFilePath();
            var filePath = arquivoNome.GetFilePath();

            System.IO.File.Copy(tempPath, filePath, true);

            var fileInfo = new FileInfo(tempPath);
            var arquivo = new Arquivo
            {
                Nome = arquivoNome,
                Titulo = titulo,
                Data = fileInfo.CreationTime,
                Extensao = fileInfo.Extension,
                Tamanho = fileInfo.Length
            };
            
            return arquivo;
        }
        #endregion

        #region Foto
        public virtual ActionResult Foto(string nomeFoto)
        {
            var foto = BuscaFoto(nomeFoto);
            if (string.IsNullOrEmpty(foto) == false)
                return File(foto);

            return new EmptyResult();
        }
        #endregion

        #region Thumbnail
        public virtual ActionResult Thumbnail(string nomeFoto, int width, int height)
        {
            var filename = BuscaFoto(nomeFoto);
            if (string.IsNullOrEmpty(filename) == false)
            {
                var image = Image.FromFile(filename);
                var thumbnail = image.CreateThumbnail(width, height);

                return File(thumbnail.ToByteArray(), "image/jpeg");
            }

            return new EmptyResult();
        }
        #endregion

        #endregion

        #region Métodos

        #region BuscaFoto
        /// <summary>
        /// Busca uma foto pelo nome, dentro da pasta Files e Temp
        /// </summary>
        private string BuscaFoto(string nomeFoto)
        {
            var foto = nomeFoto.GetFilePath();
            if (System.IO.File.Exists(foto))
                return foto;

            var tempFoto = nomeFoto.GetTempFilePath();
            if (System.IO.File.Exists(tempFoto))
                return tempFoto;

            return null;
        }
        #endregion

        #region ConcatRandomQueryString
        /// <summary>
        /// Concatena uma querystring randômica para evitar o cache da imagem.
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public string ConcatRandomQueryString(string relativeUrl)
        {
            return relativeUrl + "?_=" + DateTime.Now.ToBinary();
        }
        #endregion

        #endregion
    }
}