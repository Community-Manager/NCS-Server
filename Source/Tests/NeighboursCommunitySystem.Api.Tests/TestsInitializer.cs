namespace NeighboursCommunitySystem.Api.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Web.Http;
    using API;
    using MyTested.WebApi;

    [TestClass]
    public class TestsInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            MyWebApi.IsUsing(config);
        }
    }
}
