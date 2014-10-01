using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Fashion.ERP.Domain;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Helpers
{
    public static class Upload
    {
        #region Variáveis
        private static readonly ILogger Logger;
        #endregion

        #region Construtores
        static Upload()
        {
            var factory = DependencyResolver.Current.GetService<ILoggerFactory>();
            Logger = factory.GetCurrentClassLogger();
        }
        #endregion

        #region Propriedades
        public static string RootTempPath
        {
            get { return Path.Combine(HttpRuntime.AppDomainAppPath, "Uploads", "Temp"); }
        }

        public static string RootTempUrl
        {
            get { return VirtualPathUtility.ToAbsolute("~/Uploads/Temp/"); }
        }

        public static string RootFilesPath
        {
            get { return Path.Combine(HttpRuntime.AppDomainAppPath, "Uploads", "Files"); }
        }

        public static string RootFilesUrl
        {
            get { return VirtualPathUtility.ToAbsolute("~/Uploads/Files/"); }
        }

        #endregion

        #region CreateTempDirectory
        public static void CreateTempDirectory(HttpSessionState session)
        {
            try
            {
                var tempUploadPath = Path.Combine(RootTempPath, session.SessionID);

                if (Directory.Exists(tempUploadPath))
                    Directory.Delete(tempUploadPath, true);

                Directory.CreateDirectory(tempUploadPath);
            }
            catch (IOException exception)
            {
                Logger.Error(exception.GetMessage());
            }
        }
        #endregion

        #region DeleteTempDirectory
        public static void DeleteTempDirectory(HttpSessionState session)
        {
            try
            {
                var tempUploadPath = Path.Combine(RootTempPath, session.SessionID);

                // Excluir os arquivos temporários desta sessão
                if (!string.IsNullOrEmpty(tempUploadPath) && Directory.Exists(tempUploadPath))
                    Directory.Delete(tempUploadPath, true);
            }
            catch (IOException exception)
            {
                Logger.Error(exception.GetMessage());
            }
        }
        #endregion

        #region DeleteAllTempDirectories
        public static void DeleteAllTempDirectories()
        {
            try
            {
                // Excluir os arquivos temporários
                foreach (var file in Directory.EnumerateFiles(RootTempPath))
                    File.Delete(file);

                // Excluir os diretórios temporários
                foreach (var directory in Directory.EnumerateDirectories(RootTempPath))
                    Directory.Delete(directory, true);
            }
            catch (IOException exception)
            {
                Logger.Error(exception.GetMessage());
            }
        }
        #endregion

        #region DeleteOldFiles
        /// <summary>
        /// Exclui os arquivos que não tem mais relação no banco de dados.
        /// </summary>
        public static void DeleteOldFiles()
        {
            // Excluir os arquivos que não tem ligação com o banco de dados
            var arquivoRepository = DependencyResolver.Current.GetService<IRepository<Arquivo>>();
            var dbArquivos = arquivoRepository.Find().ToList().Select(p => p.Nome.GetFilePath());
            var discoArquivos = Directory.EnumerateFiles(RootFilesPath);

            foreach (var file in discoArquivos.Except(dbArquivos))
                File.Delete(file);
        }
        #endregion

        #region GenerateTempFilename
        public static string GenerateTempFilename(string extension)
        {
            return Path.ChangeExtension(Path.GetRandomFileName(), extension);
        }
        #endregion
    }
}