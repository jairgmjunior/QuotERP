using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Fashion.Framework.Common.Extensions;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class UploadExtensions
    {
        #region Variáveis
        private static readonly ILogger Logger;
        #endregion

        #region Construtores
        static UploadExtensions()
        {
            var factory = DependencyResolver.Current.GetService<ILoggerFactory>();
            Logger = factory.GetCurrentClassLogger();
        }
        #endregion

        #region SaveFile
        public static string SaveFile(this byte[] file, string extension)
        {
            var tempUploadPath = Path.Combine(Upload.RootTempPath, HttpContext.Current.Session.SessionID);
            var filename = Upload.GenerateTempFilename(extension);

            try
            {
                if (!Directory.Exists(tempUploadPath))
                    Directory.CreateDirectory(tempUploadPath);

                using (Stream stream = File.OpenWrite(Path.Combine(tempUploadPath, filename)))
                    stream.Write(file, 0, file.Length);
            }
            catch (IOException exception)
            {
                Logger.Error(exception.GetMessage());
            }

            return GetTempFileUrl(filename);
        }
        #endregion

        #region GetTempFileUrl

        public static string GetTempFileUrl(this string filename)
        {
            return Upload.RootTempUrl + HttpContext.Current.Session.SessionID + "/" + Path.GetFileName(filename);
        }

        #endregion

        #region GetTempFilePath
        /// <summary>
        /// Anexa ou gera o nome de um arquivo na pasta ~/Uploads/Temp/"SessionID", e retorna o caminho completo para o arquivo.
        /// </summary>
        /// <param name="filename">Nome do arquivo ou nulo para ser gerado um aleatoriamente.</param>
        /// <returns>O caminho para um arquivo.</returns>
        public static string GetTempFilePath(this string filename)
        {
            var tempFilePath = Path.Combine(Upload.RootTempPath, HttpContext.Current.Session.SessionID, Path.GetFileName(filename) ?? filename);

            var dirTemp = Path.GetDirectoryName(tempFilePath);
            if (dirTemp != null && !Directory.Exists(dirTemp))
                Directory.CreateDirectory(dirTemp);

            return tempFilePath;
        }

        #endregion

        #region GetFileUrl

        public static string GetFileUrl(this string filename)
        {
            return Upload.RootFilesUrl + Path.GetFileName(filename);
        }

        #endregion

        #region GetFilePath

        /// <summary>
        /// Anexa ou gera o nome de um arquivo na pasta ~/Uploads/Files/, e retorna o caminho completo para o arquivo.
        /// </summary>
        /// <param name="filename">Nome do arquivo ou nulo para ser gerado um aleatoriamente.</param>
        /// <returns>O caminho para um arquivo.</returns>
        public static string GetFilePath(this string filename)
        {
            return Path.Combine(Upload.RootFilesPath, Path.GetFileName(filename) ?? filename);
        }

        #endregion
    }
}