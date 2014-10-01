using System;
using System.IO;
using System.IO.Packaging;
using System.Net.Mime;
using System.Reflection;
using Microsoft.Deployment.WindowsInstaller;

namespace Fashion.ERP.Setup.CustomAction
{
    public static class Helpers
    {
        #region GetMessage
        /// <summary>
        /// Retorna recursivamente todas as mensagens da excessão.
        /// </summary>
        public static string GetMessage(this Exception exception)
        {
            if (exception == null)
                return string.Empty;

            if (exception.InnerException != null)
                return string.Format("{0}\r\n > {1} ",
                    exception.Message,
                    GetMessage(exception.InnerException));

            if (exception is ReflectionTypeLoadException)
            {
                var typeLoadException = exception as ReflectionTypeLoadException;

                var message = string.Empty;

                foreach (var ex in typeLoadException.LoaderExceptions)
                    message += string.Format("{0}\r\n > {1} ", message, GetMessage(ex));

                return message;
            }

            return exception.Message;
        }
        #endregion

        #region ShowErrorMessage
        public static void ShowErrorMessage(this Session session, Exception exception)
        {
            ShowErrorMessage(session, "Exception: {0}\r\nStacktrace: {1}", exception.GetMessage(), exception.StackTrace);
        }
        #endregion

        #region ShowErrorMessage
        public static void ShowErrorMessage(this Session session, string message, params object[] args)
        {
            session.Log("Ocorreu um erro: {0}", message);

            using (var record = new Record())
            {
                record.FormatString = string.Format(message, args);
                session.Message(InstallMessage.User, record);
            }
        }
        #endregion

        #region ZipFiles
        public static void ZipFiles(string zipPath, string sourceDirectory)
        {
            var di = new DirectoryInfo(sourceDirectory);

            // Open the zip file if it exists, else create a new one
            using (var zip = Package.Open(zipPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                foreach (var file in di.GetFiles())
                {
                    AddToArchive(zip, file.FullName);
                }
                //CreatePackageForEachFolder(zip, di, di.Name);
            }
        }
        #endregion

        #region AddToArchive
        private static void AddToArchive(Package zip, string fileToAdd)
        {
            var uriFileName = fileToAdd.Replace(" ", "_");

            // A Uri always starts with a forward slash "/" 
            var zipUri = string.Concat("/", Path.GetFileName(uriFileName));

            var pkgPart = zip.CreatePart(new Uri(zipUri, UriKind.Relative),
                MediaTypeNames.Application.Zip, CompressionOption.Normal);

            if (pkgPart != null)
            {
                var bites = File.ReadAllBytes(fileToAdd);

                // Compress and write the bytes to the zip file 
                pkgPart.GetStream().Write(bites, 0, bites.Length);
            }
        }
        #endregion

        #region CreatePackageForEachFolder
        private static void CreatePackageForEachFolder(Package package, DirectoryInfo parentDirectoryInfo, string partName)
        {
            foreach (var file in parentDirectoryInfo.GetFiles())
            {
                var fileUri = PackUriHelper.CreatePartUri(new Uri(partName + "//" + file.Name, UriKind.Relative));
                // Add the Document part to the Package
                var packagePartDocument = package.CreatePart(fileUri, MediaTypeNames.Application.Octet, CompressionOption.NotCompressed);

                // Copy the data to the Document Part
                using (var fileStream = new FileStream(parentDirectoryInfo.FullName + "//" + file.Name, FileMode.Open, FileAccess.Read))
                {
                    if (packagePartDocument != null)
                        CopyStream(fileStream, packagePartDocument.GetStream());
                }
            }

            foreach (var dInfo in parentDirectoryInfo.GetDirectories())
            {
                CreatePackageForEachFolder(package, dInfo, partName + "//" + dInfo.Name);
            }
        }
        #endregion

        #region CopyStream
        private static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            var buf = new byte[bufSize];
            int bytesRead;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }
        #endregion
    }
}