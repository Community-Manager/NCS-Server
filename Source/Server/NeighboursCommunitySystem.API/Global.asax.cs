namespace NeighboursCommunitySystem.API
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutoMapperConfig.RegisterMappings(Assembly.Load("NeighboursCommunitySystem.DtoModels"));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}