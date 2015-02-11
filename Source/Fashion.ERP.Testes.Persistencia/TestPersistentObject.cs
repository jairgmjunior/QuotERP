using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Fashion.Framework.Common.Base;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork;
using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;

namespace Fashion.ERP.Testes.Persistencia
{
    public abstract class TestPersistentObject<T> where T : class, IPersistentObject, new()
    {
        private T _persistentObject;
        protected FabricaObjetosPersistidos FabricaObjetosPersistidos = new FabricaObjetosPersistidos();
        protected FabricaObjetos FabricaObjetos = new FabricaObjetos();

        public IRepository<T> Repository
        {
            get
            {
                return RepositoryFactory.Create<T>();
            }
        }

        [TestFixtureSetUp]
        public void RunBeforeAnyTests()
        {
            ConfigureHttpContext();

            Init();

            _persistentObject = GetPersistentObject();
        }

        [TestFixtureTearDown]
        public void RunAfterAnyTests()
        {
            _persistentObject = null;
            Cleanup();
        }

        public abstract T GetPersistentObject();
        public abstract void Init();
        public abstract void Cleanup();

        public virtual void DesfacaAssociacoes(T persistentObject)
        {
        }

        [Test(Description = "Salva, Consulta e Exclui objeto no banco de dados")]
        [UnitOfWork]
        public void TestCRUD()
        {
            T persistentObject = default(T);

            try
            {
                persistentObject = Repository.Save(_persistentObject);
                Session.Current.Flush();

                if (persistentObject.Id != null)
                {
                    persistentObject = Repository.Load(persistentObject.Id.Value);
                    Assert.NotNull(persistentObject);
                }
                else
                    Assert.Fail("A entidade não foi salva no banco.");

                VerifiqueObjetoPersistido(persistentObject);

                DesfacaAssociacoes(persistentObject);
                Repository.Delete(persistentObject);
                Session.Current.Flush();

                persistentObject = Repository.Find(item => item.Id == persistentObject.Id).FirstOrDefault();
                Assert.IsNull(persistentObject);
            }
            catch (Exception ex)
            {
                if (persistentObject != null && persistentObject.Id != null)
                {
                    Repository.Delete(persistentObject);
                    Session.Current.Flush();
                }
                Assert.Fail("Exceção: " + ex.Message);
            }
        }

        private void VerifiqueObjetoPersistido(T persistentObject)
        {
            var compareLogic = new CompareLogic();
            compareLogic.Config.MembersToIgnore.Add("Id");
            compareLogic.Config.MembersToIgnore.Add("IdEmpresa");
            compareLogic.Config.MembersToIgnore.Add("IdTenant");
            compareLogic.Config.MembersToIgnore.Add("CpfCnpj");
            compareLogic.Config.MembersToIgnore.Add("DataAlteracao");
            compareLogic.Config.IgnoreObjectTypes = true;
            var result = compareLogic.Compare(GetPersistentObject(), persistentObject);
            Assert.IsTrue(result.AreEqual, result.DifferencesString);
        }

        #region ConfigureHttpContext

        private void ConfigureHttpContext()
        {
            HttpContext.Current = FakeHttpContext();
            SessaoLogada.InsiraValoresLogados(1, 1);
        }

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://fashion/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                        .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
        #endregion
    }
}
