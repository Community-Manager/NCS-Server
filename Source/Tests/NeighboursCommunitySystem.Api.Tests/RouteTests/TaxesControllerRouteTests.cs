namespace NeighboursCommunitySystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Web.Http;
    using API;
    using MyTested.WebApi;
    using API.Controllers;

    [TestClass]
    public class TaxesControllerRouteTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            MyWebApi.IsUsing(config);
        }
        
        //[TestMethod]
        //public void GetWithIdShouldMapCorrectly()
        //{
        //    MyWebApi
        //        .Routes()
        //        .ShouldMap("api/taxes/get/1")
        //        .To<TaxesController>(c => c.Get(1));
        //}
    }
}
