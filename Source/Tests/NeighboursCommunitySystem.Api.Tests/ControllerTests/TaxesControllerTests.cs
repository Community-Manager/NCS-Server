namespace NeighboursCommunitySystem.Api.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using API.Controllers;
    using AutoMapper;
    using Server.DataTransferModels.Taxes;
    using Models;
    using System.Collections.Generic;

    [TestClass]
    public class TaxesControllerTests
    {
        private object[] dependencies;

        [TestInitialize]
        public void InitializeDependencies()
        {
            this.dependencies = new object[2];
            dependencies[0] = TestObjectFactory.GetTaxesService();
            dependencies[1] = TestObjectFactory.GetCommunitiesService();

            Mapper.CreateMap<Tax, TaxDataTransferModel>();
        }

        [TestMethod]
        public void GetWithValidTaxIdShouldReturnOk()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Get(TestObjectFactory.validId))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<TaxDataTransferModel>()
                .Passing(t =>
                {
                    Assert.AreEqual(TestObjectFactory.Taxes[0].Name, t.Name);
                    Assert.AreEqual(TestObjectFactory.Taxes[0].Price, t.Price);
                });
        }

        [TestMethod]
        public void GetShouldHaveAuthorizedAttribute()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Get(TestObjectFactory.validId))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [TestMethod]
        public void GetWithInvalidTaxIdShouldReturnBadRequest()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Get(TestObjectFactory.invalidId))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void GetWithInvalidUserCommunityShouldReturnUnauthorized()
        {            
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Get(TestObjectFactory.validTaxIdInvalidCommunity))
                .ShouldReturn()
                .Unauthorized();
        }

        [TestMethod]
        public void GetByCommunityWithInvalidIdShouldReturnUnauthorized()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Community(TestObjectFactory.validTaxIdInvalidCommunity))
                .ShouldReturn()
                .Unauthorized();
        }

        [TestMethod]
        public void GetByCommunityWithValidIdShouldRetrurnOk()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Community(TestObjectFactory.validId))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<TaxDataTransferModel>>();
        }

        [TestMethod]
        public void GetByCommunityShouldHaveAuthorizedAttribute()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Community(TestObjectFactory.validId))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [TestMethod]
        public void PostWithValidModelShoudReturnOk()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Post(TestObjectFactory.GetValidTaxRequestModel()))
                .ShouldReturn()
                .Ok();
        }

        [TestMethod]
        public void PostWithInvalidCommunityShoudReturnUnauthorized()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Post(TestObjectFactory.GetValidTaxRequestModelWithInvalidCommunity()))
                .ShouldReturn()
                .Unauthorized();
        }

        [TestMethod]
        public void PostShouldHaveAuthorizedAttribute()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(this.dependencies)
                .Calling(c => c.Post(TestObjectFactory.GetValidTaxRequestModel()))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }
    }
}

