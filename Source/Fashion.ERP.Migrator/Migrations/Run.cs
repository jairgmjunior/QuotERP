using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;

namespace Fashion.ERP.Migrator
{
    public class Run
    {
        public static void Main(string[] args)
        {
            //migrate -c "server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456;" -db sqlserver2012 -a "D:\Victor\Projetos\Fashion.ERP\Source\Fashion.ERP.Migrator\bin\Debug\Fashion.ERP.Migrator.dll" -t migrate -o -of migrated.sql
            string connectionString = @"Server=.\SQLEXPRESS;Database=FashionERP;User Id=sa;Password=123456;";
            Announcer announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            announcer.ShowSql = true;

            Assembly assembly = Assembly.GetExecutingAssembly();
            IRunnerContext migrationContext = new RunnerContext(announcer);

            var options = new ProcessorOptions
            {
                PreviewOnly = false,  // set to true to see the SQL
                Timeout = 60
            };
            var factory = new SqlServer2012ProcessorFactory();
            IMigrationProcessor processor = factory.Create(connectionString, announcer, options);
            
            var runner = new MigrationRunner(assembly, migrationContext, processor);
            
            runner.MigrateUp(true);

            // Or go back down
            //runner.MigrateDown(0);
        }
    }
}
