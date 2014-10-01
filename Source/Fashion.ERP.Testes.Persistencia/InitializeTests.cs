using System;
using Fashion.Framework.UnitOfWork;
using NUnit.Framework;

[SetUpFixture]
public class InitializeTests
{
    [SetUp]
    public virtual void RunBeforeAnyTests()
    {
        log4net.Config.XmlConfigurator.Configure(); 
        HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

        try
        {
            //Session.GenerateDatabase();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao gerar bando de dados: " + ex.Message);
        }
    }

    [TearDown]
    public virtual void RunAfterAnyTests()
    {
        //excluabanco
    }
}

