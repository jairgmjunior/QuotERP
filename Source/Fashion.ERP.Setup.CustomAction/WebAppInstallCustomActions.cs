/*------------------------------------------------------------------------------
 * This is a very simple example of filling in a Windows Installer combo boxes
 * with the names of existing web sites on an IIS 7 and higher server and the
 * application pools
 * Major credit to :
 * Jon Torresdal 
 * http://blog.torresdal.net/2008/10/24/WiXAndDTFUsingACustomActionToListAvailableWebSitesOnIIS.aspx
 * "Dan" [Especially for going back to fix his own question!]
 * http://stackoverflow.com/questions/1373600/how-do-i-populate-a-combobox-at-install-time-in-wix
 *----------------------------------------------------------------------------*/

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Configuration;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Web.Administration;
using System.Security.Principal;

namespace Fashion.ERP.Setup.CustomAction
{
    /// <summary>
    /// Contains the custom actions necessary to work with the 
    /// WebVDirInstallDlg.WXS file.
    /// </summary>
    public static class CustomActions
    {
        #region Variáveis

        private const string DbMigratorName = "Fashion.ERP.Migrator.dll";
        private const string ConnectionStringName = "SQLServerCon";
        private const string DbType = "SqlServer";
        private const string ProviderName = "System.Data.SqlClient";

        #endregion

        #region EnumerateIisWebSitesAndAppPools
        /// <summary>
        /// Adds the II7 web sites and the application pool names to the 
        /// ComboBox table in the MSI file.
        /// </summary>
        /// <param name="session">
        /// The installer session.
        /// </param>
        /// <returns>
        /// Always returns ActionResult.Success, otherwise rethrows the error
        /// encountered.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="session"/> parameter is null.
        /// </exception>
        [CustomAction]
        public static ActionResult EnumerateIisWebSitesAndAppPools(Session session)
        {
            if (null == session)
            {
                throw new ArgumentNullException("session");
            }

            session.Log("EnumerateIISWebSitesAndAppPools: Begin");

            // Check if running with admin rights and if not, log a message to
            // let them know why it's failing.
            if (false == HasAdminRights())
            {
                session.Log("EnumerateIISWebSitesAndAppPools: ATTEMPTING TO RUN WITHOUT ADMIN RIGHTS");

                var record = new Record
                {
                    FormatString = string.Format("O Setup deve ser executado como administrador!")
                };

                session.Message(InstallMessage.User, record);

                return ActionResult.Failure;
            }

            session.Log("EnumerateIISWebSitesAndAppPools: Getting the IIS 7 management object");
            ActionResult result;
            using (var iisManager = new ServerManager())
            {
                result = EnumSitesIntoComboBox(session, iisManager);
                if (ActionResult.Success == result)
                {
                    result = EnumAppPoolsIntoComboBox(session, iisManager);
                }
            }

            session.Log("EnumerateIISWebSitesAndAppPools: End");
            return result;
        }
        #endregion

        #region SetInstallDirBasedOnSelectedWebSite
        /// <summary>
        /// Sets the INSTALLDIR property to the directory where the web site 
        /// defaults to.
        /// </summary>
        /// <param name="session">
        /// The installer session.
        /// </param>
        /// <returns>
        /// Returns ActionResult.Success if the web site properly exists. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="session"/> parameter is null.
        /// </exception>
        [CustomAction]
        public static ActionResult SetInstallDirBasedOnSelectedWebSite(Session session)
        {
            if (null == session)
                throw new ArgumentNullException("session");

            try
            {
                session.Log("SetInstallDir: Begin");

                // Let's get the selected website.
                String webSite = session["WEBSITE_NAME"];
                session.Log("SetInstallDir: Working with the web site: {0}", webSite);

                // Grab that web sites based physical directory and get it's "/"
                // (base path physical directory).
                String basePath;
                using (var iisManager = new ServerManager())
                {
                    Site site = iisManager.Sites[webSite];
                    basePath = site.Applications["/"].VirtualDirectories["/"].PhysicalPath;
                }

                session.Log("SetInstallDir: Physical path : {0}", basePath);

                // Environment variables are used in IIS7 so expand them.
                basePath = Environment.ExpandEnvironmentVariables(basePath);
                session["INSTALLLOCATION"] = basePath;
            }
            catch (Exception ex)
            {
                session.Log("SetInstallDir: exception: {0}", ex.Message);
                throw;
            }

            return ActionResult.Success;
        }
        #endregion

        #region SetAppPoolNameToWebSiteDefault
        /// <summary>
        /// The custom action to get the application pool for the selected web
        /// site the user wants to install to in WebVDirInstallDlg.WXS. This 
        /// will set the APP_POOL_NAME property.
        /// </summary>
        /// <param name="session">
        /// The installer session.
        /// </param>
        /// <returns>
        /// Returns ActionResult.Success if the web site properly exists. 
        /// ActionResult.Failure otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="session"/> parameter is null.
        /// </exception>
        [CustomAction]
        public static ActionResult SetAppPoolNameToWebSiteDefault(Session session)
        {
            if (null == session)
            {
                throw new ArgumentNullException("session");
            }

            try
            {
                // Debugger.Break();
                session.Log("SetAppPoolName: Begin");

                // Let's get the selected website.
                String webSite = session["WEBSITE_NAME"];
                session.Log("SetAppPoolName: Working with the web site: {0}", webSite);

                session.Log("SetAppPoolName: Getting the IIS 7 ServerManager");
                using (var iisManager = new ServerManager())
                {

                    session.Log("SetAppPoolName: Getting the app pool.");
                    string appPool = iisManager.Sites[webSite].Applications["/"].ApplicationPoolName;
                    session.Log("SetAppPoolName: Found the app pool: {0}", appPool);
                    session["APP_POOL_NAME"] = appPool;
                    session.Log("SetAppPoolName: Set the APP_POOL_NAME property.");
                }
            }
            catch (Exception ex)
            {
                session.Log("SetAppPoolName: exception: {0}",
                            ex.Message);
                throw;
            }

            return ActionResult.Success;
        }
        #endregion

        #region EnumAppPoolsIntoComboBox
        private static ActionResult EnumAppPoolsIntoComboBox(Session session, ServerManager iisManager)
        {
            session.Log("EnumAppPools: Begin");
            try
            {
                // Grab the combo box.
                View view = session.Database.OpenView("SELECT * FROM ComboBox WHERE ComboBox.Property='APP_POOL_NAME'");
                view.Execute();

                Int32 index = 1;
                session.Log("EnumAppPools: Enumerating the app pools");
                foreach (var pool in iisManager.ApplicationPools)
                {
                    // Create a record for this app pool. All I care about is
                    // the name so use it for fields three and four.
                    session.Log("EnumAppPools: Processing app pool: {0}", pool.Name);
                    Record record = session.Database.CreateRecord(4);
                    record.SetString(1, "APP_POOL_NAME");
                    record.SetInteger(2, index);
                    record.SetString(3, pool.Name);
                    record.SetString(4, pool.Name);

                    session.Log("EnumAppPools: Adding app pool record");
                    view.Modify(ViewModifyMode.InsertTemporary, record);
                    index++;
                }

                view.Close();

                session.Log("EnumAppPools: End");
            }
            catch (Exception ex)
            {
                session.Log("EnumAppPools exception: {0}", ex.Message);
                throw;
            }

            return ActionResult.Success;
        }
        #endregion

        #region EnumSitesIntoComboBox
        private static ActionResult EnumSitesIntoComboBox(Session session, ServerManager iisManager)
        {
            try
            {
                // Debugger.Break();
                session.Log("EnumSites: Begin");

                // Grab the combo box but make sure I'm getting only the one 
                // from WebAppInstallDlg.
                View view = session.Database.OpenView("SELECT * FROM ComboBox WHERE ComboBox.Property='WEBSITE_NAME'");
                view.Execute();

                Int32 index = 1;
                session.Log("EnumSites: Enumerating the sites");
                foreach (Site site in iisManager.Sites)
                {
                    // Create a record for this web site. All I care about is
                    // the name so use it for fields three and four.
                    session.Log("EnumSites: Processing site: {0}", site.Name);
                    Record record = session.Database.CreateRecord(4);
                    record.SetString(1, "WEBSITE_NAME");
                    record.SetInteger(2, index);
                    record.SetString(3, site.Name);
                    record.SetString(4, site.Name);

                    session.Log("EnumSites: Adding record");
                    view.Modify(ViewModifyMode.InsertTemporary, record);
                    index++;
                }

                view.Close();

                session.Log("EnumSites: End");
            }
            catch (Exception ex)
            {
                session.Log("EnumSites: exception: {0}", ex.Message);
                throw;
            }

            return ActionResult.Success;
        }
        #endregion

        #region HasAdminRights
        static bool HasAdminRights()
        {
            var identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
        #endregion

        #region MigrateDatabase
        [CustomAction]
        public static ActionResult MigrateDatabase(Session session)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Launch(); // Launches and attaches a debugger to the process
                Debugger.Break(); // Signals a breakpoint to an attached debugger.
            }
            session.Log("Inicia método MigrateDatabase");

            try
            {
                var connectionString = session["CONNECTION_STRING"];
                var installLocation = session["INSTALLLOCATION"];

                // Configuração do Web.Config
                session.Log("Inicia configuração do Web.Config.");

                if (File.Exists(installLocation + "Web.Config"))
                    SaveConnectionString(installLocation, connectionString);
                else
                    session.ShowErrorMessage("Arquivo web.config não encontrado: '" + installLocation + "'");

                session.Log("Configuração do Web.Config bem sucedido!");

                // Migração do banco de dados
                session.Log("Inicia migração do banco de dados.");

                var log = ExecuteMigrateDatabase(connectionString);

                if (log.Length > 0)
                    session.Log("Log Migrage: {0}", log);

                session.Log("Migração do banco de dados bem sucedido!");
            }
            catch (Exception exception)
            {
                session.ShowErrorMessage(exception);
                return ActionResult.Failure;
            }
            session.Log("Finaliza método MigrateDatabase");

            return ActionResult.Success;
        }
        #endregion

        #region SaveConnectionString
        /// <summary>
        /// Salva a string de conexão no arquivo de configuração especificado.
        /// </summary>
        /// <param name="webConfigPath">Endereço do Web.config.</param>
        /// <param name="connectionString">String de conexão.</param>
        private static void SaveConnectionString(string webConfigPath, string connectionString)
        {
            var vdm = new VirtualDirectoryMapping(webConfigPath, true);
            var wcfm = new WebConfigurationFileMap();
            wcfm.VirtualDirectories.Add("/", vdm);

            // Get the Web application configuration object.
            var config = System.Web.Configuration.WebConfigurationManager.OpenMappedWebConfiguration(wcfm, "/");

            var section = config.GetSection("connectionStrings") as ConnectionStringsSection;

            if (section != null && !section.SectionInformation.IsProtected)
            {
                section.ConnectionStrings[ConnectionStringName].ConnectionString = connectionString;
                section.ConnectionStrings[ConnectionStringName].ProviderName = ProviderName;

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(section.SectionInformation.SectionName);
            }
        }
        #endregion

        #region MigrateDatabase
        private static string ExecuteMigrateDatabase(string connectionString)
        {
            var log = new StringBuilder();

            var context = new RunnerContext(new TextWriterAnnouncer(new StringWriter(log)))
            {
                Database = DbType,
                Connection = connectionString,
                Target = DbMigratorName,
                PreviewOnly = false,
                Namespace = null,
                Task = "migrate",
                //Version = version,
                Steps = 1,
                WorkingDirectory = null,
                Profile = null
            };

            new TaskExecutor(context).Execute();

            return log.ToString();
        }
        #endregion

        #region TestConnection
        [CustomAction]
        public static ActionResult TestConnection(Session session)
        {
            //Debugger.Launch(); // Launches and attaches a debugger to the process
            //Debugger.Break(); // Signals a breakpoint to an attached debugger.
            session.Log("Inicia teste de conexão.");

            session["TEST_CONNECTION"] = "0";
            var connectionString = session["CONNECTION_STRING"];

            try
            {
                using (var conn = new SqlConnection(connectionString))
                    conn.Open();

                session.Log("Conexão bem sucedida!");
                session["TEST_CONNECTION"] = "1";
            }
            catch (Exception exception)
            {
                session.Log("Falha ao conectar ao banco de dados!\r\n{0}", connectionString);

                session.ShowErrorMessage(exception);
            }

            return ActionResult.Success;
        }
        #endregion

        #region Backup
        [CustomAction]
        public static ActionResult Backup(Session session)
        {
            //Debugger.Launch(); // Launches and attaches a debugger to the process

            session.Log("Inicia método Backup");

            try
            {
                var connectionString = session["CONNECTION_STRING"];
                var backupLocation = session["BACKUP_LOCATION"];
                var installLocation = session["INSTALLLOCATION"];
                var filesDirectory = Path.Combine(installLocation, "Uploads\\Files");
                //var filesDirectory = installLocation;

                if (Directory.Exists(backupLocation) == false)
                {
                    session.ShowErrorMessage("O diretório '" + backupLocation + "' não foi encontrado. O backup não será realizado ");
                    return ActionResult.Success;
                }

                if (string.IsNullOrWhiteSpace(backupLocation))
                {
                    session.Log("Backup não realizado!");
                    return ActionResult.Success;
                }

                backupLocation = string.Format("{0}\\{1:yyyyMMddhhmmss}", backupLocation, DateTime.Now);

                if (Directory.Exists(backupLocation) == false)
                    Directory.CreateDirectory(backupLocation);

                var log = BackupDatabase(connectionString, backupLocation);
                log += Environment.NewLine + BackupFiles(backupLocation, filesDirectory);

                if (log.Length > 0)
                    session.Log("Log Backup: {0}", log);

                session.Log("Backup bem sucedido!");

            }
            catch (Exception exception)
            {
                session.ShowErrorMessage(exception);
                return ActionResult.Failure;
            }
            session.Log("Finaliza método Backup");

            return ActionResult.Success;
        }
        #endregion

        #region BackupDatabase
        private static string BackupDatabase(string connectionString, string foldername)
        {
            string log = string.Empty;

            try
            {
                var sqlString = new SqlConnectionStringBuilder(connectionString);
                var bkpName = Path.Combine(foldername, "FashionERP.bak");

                var sqlBackup = string.Format(@"BACKUP DATABASE [{0}] TO DISK = N'{1}' WITH NOFORMAT, INIT,  NAME = N'NovaModa-Full', SKIP",
                                              sqlString.InitialCatalog, bkpName);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(sqlBackup, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                log = sqlException.GetMessage();
            }
            
            return log;
        }
        #endregion

        #region BackupFiles
        private static string BackupFiles(string foldername, string filesDirectory)
        {
            string log = string.Empty;

            try
            {
                var zipPath = Path.Combine(foldername, "Arquivos.zip");
                Helpers.ZipFiles(zipPath, filesDirectory);
            }
            catch (Exception exception)
            {
                log = exception.GetMessage();
            }

            return log;
        }
        #endregion
    }
}
