namespace NeighboursCommunitySystem.Api.Tests.IntegrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;
    using System.Web;
    using API.Controllers;

    [TestClass]
    public class CommunityIntegrationTests
    {
        private HttpServer httpServer;
        private HttpMessageInvoker httpInvoker;
        private HttpConfiguration config;

        [TestInitialize]
        public void IntegrationInit()
        {
            config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            httpServer = new HttpServer(config);
            httpInvoker = new HttpMessageInvoker(httpServer);
        }

        [TestMethod]
        public void GetShouldReturnCorrectResponse()
        {
            using (httpInvoker)
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://test.com/api/communities"),
                    Method = HttpMethod.Get
                };

                var result = httpInvoker.SendAsync(request, CancellationToken.None).Result;

                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [TestMethod]
        public void GetByInvalidUserIdShouldReturnCorrectResponse()
        {
            using (httpInvoker)
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://test.com/api/communities/getbyid/invalidUser"),
                    Method = HttpMethod.Get
                };

                var result = httpInvoker.SendAsync(request, CancellationToken.None).Result;

                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            }
        }
    }
}
