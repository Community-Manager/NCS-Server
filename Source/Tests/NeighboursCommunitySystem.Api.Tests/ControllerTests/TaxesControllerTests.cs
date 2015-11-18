namespace NeighboursCommunitySystem.Api.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using API.Controllers;
    using AutoMapper;
    using Api;
    using Server.DataTransferModels.Taxes;
    using Models;

    [TestClass]
    public class TaxesControllerTests
    {
        [TestMethod]
        public void GettingWithValidIdShouldReturnOk()
        {
            Mapper.CreateMap<Tax, TaxDataTransferModel>();

            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(TestObjectFactory.GetTaxesService(), TestObjectFactory.GetCommunitiesService())
                .Calling(c => c.Get(TestObjectFactory.validId))               
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<TaxDataTransferModel>();
        }

        [TestMethod]
        public void GettingWithInvalidIdShouldReturnBadRequest()
        {
            MyWebApi
                .Controller<TaxesController>()
                .WithResolvedDependencies(TestObjectFactory.GetTaxesService(), TestObjectFactory.GetCommunitiesService())
                .Calling(c => c.Get(TestObjectFactory.invalidId))
                .ShouldReturn()
                .BadRequest();
        }
    }
}

