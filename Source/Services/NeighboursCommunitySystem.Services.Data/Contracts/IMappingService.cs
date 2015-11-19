
namespace NeighboursCommunitySystem.Services.Data.Contracts
{
    public interface IMappingService : IService
    {
        T Map<T>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
