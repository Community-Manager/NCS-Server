namespace NeighboursCommunitySystem.Api.Tests.ControllerTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using API.Controllers;
    using AutoMapper;
    using Server.DataTransferModels.Taxes;
    using Models;
    using System.Collections.Generic;
    using Server.DataTransferModels.Communities;

    [TestClass]
    public class CommunitiesControllerTests
    {
        [TestMethod]
        public void GetShouldReturnOk()
        {
            MyWebApi
                .Controller<CommunitiesController>()
                .WithResolvedDependencyFor(TestObjectFactory.GetCommunitiesService())
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<CommunityDataTransferModel>>();
        }

        [TestMethod]
        public void GetWithIdShouldReturnOk()
        {
            MyWebApi
                .Controller<CommunitiesController>()
                .WithResolvedDependencyFor(TestObjectFactory.GetCommunitiesService())
                .Calling(c => c.GetById("TestUserId"))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<CommunityDataTransferModel>>()
                .Passing(m =>
                {
                    Assert.AreEqual(m[0].Name, TestObjectFactory.Communities[0].Name);
                });
        }

        [TestMethod]
        public void PostByAdminShoudReturnOk()
        {
            MyWebApi
                .Controller<CommunitiesController>()
                .WithResolvedDependencyFor(TestObjectFactory.GetCommunitiesService())
                .Calling(c => c.PostCommunityByLoggedAdmin(new CommunityDataTransferModel()))
                .ShouldReturn()
                .Ok();
        }

        [TestMethod]
        public void PostByAdminShouldHaveAuthorizedAttribute()
        {
            MyWebApi
                .Controller<CommunitiesController>()
                .WithResolvedDependencyFor(TestObjectFactory.GetCommunitiesService())
                .Calling(c => c.PostCommunityByLoggedAdmin(new CommunityDataTransferModel()))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }
    }
}
