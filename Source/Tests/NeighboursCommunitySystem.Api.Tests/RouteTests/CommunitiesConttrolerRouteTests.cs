namespace NeighboursCommunitySystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using API.Controllers;

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
    }
}
