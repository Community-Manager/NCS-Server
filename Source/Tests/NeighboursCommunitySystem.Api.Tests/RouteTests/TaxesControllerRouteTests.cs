namespace NeighboursCommunitySystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net.Http;
    using System.Web.Http;
    using API;
    using MyTested.WebApi;
    using API.Controllers;
    using Server.DataTransferModels.Taxes;
    using System;

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

        [TestMethod]
        public void GetWithIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/taxes/get/1")
                .To<TaxesController>(c => c.Get(1));
        }

        [TestMethod]
        public void GetWithCommunityIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/taxes/community/1")
                .To<TaxesController>(c => c.Community(1));
        }

        [TestMethod]
        public void PostShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/taxes")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{ ""Name"": ""Test tax"", ""Price"": 1 }")
                .To<TaxesController>(c => c.Post(new TaxRequestTransferModel
                {
                    Name = "Test tax",
                    Price = 1,
                }));
        }

        [TestMethod]
        public void GetAvailableShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/taxes/available/1")
                .To<TaxesController>(c => c.Available(1));
        }

        [TestMethod]
        public void GetExpiredShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/taxes/expired/1")
                .To<TaxesController>(c => c.Expired(1));
        }
    }
}
