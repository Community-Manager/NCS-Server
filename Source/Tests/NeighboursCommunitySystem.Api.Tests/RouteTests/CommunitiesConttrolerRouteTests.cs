namespace NeighboursCommunitySystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using API.Controllers;
    using Server.DataTransferModels.Communities;
    using System.Net.Http;

    [TestClass]
    public class CommunitiesConttrolerRouteTests
    {
        [TestMethod]
        public void GetShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/communities")
                .To<CommunitiesController>(c => c.Get());
        }

        [TestMethod]
        public void GetWithUserIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/communities/getbyid/test")
                .To<CommunitiesController>(c => c.GetById("test"));
        }

        [TestMethod]
        public void PostShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/communities/post")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{ ""FirstName"": ""Test""}")
                .To<CommunitiesController>(c => c.Post(new CommunityWithAdminDataTransferModel()
                {
                    FirstName = "Test",
                }));
        }

        [TestMethod]
        public void PostByAdminShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/communities/PostCommunityByLoggedAdmin")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{ ""Name"": ""Test"" , ""Description"": ""Test Description"" }")
                .To<CommunitiesController>(c => c.PostCommunityByLoggedAdmin(new CommunityDataTransferModel()
                {
                    Name = "Test",
                    Description = "Test Description"
                }));
        }
    }
}
