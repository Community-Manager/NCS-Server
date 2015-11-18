namespace NeighboursCommunitySystem.Api.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Moq;
    using Services.Data.Contracts;

    internal class TestObjectFactory
    {
        public const int validId = 1;
        public const int validTaxIdInvalidCommunity = 2;
        public const int invalidId = 3;

        private static readonly IQueryable<Tax> taxes = new List<Tax>
        {
            new Tax
            {
                Name = "Test Tax 1",
                Price = 123,
                Description = "With valid community",
                CommunityId = validId
            },
            new Tax
            {
                Name = "Test Tax 2",
                Price = 123,
                Description = "With invalid community",
                CommunityId = invalidId
            }
        }.AsQueryable();

        public static ITaxesService GetTaxesService()
        {
            var taxesServices = new Mock<ITaxesService>();

            taxesServices
                .Setup(t => t.All())
                .Returns(taxes);

            taxesServices
                .Setup(t => t.GetById(It.Is<int>(id => id == validId)))
                .Returns(taxes.First());

            taxesServices
                .Setup(t => t.GetById(It.Is<int>(id => id == validTaxIdInvalidCommunity)))
                .Returns(taxes.Last());

            taxesServices
                .Setup(t => t.GetById(It.Is<int>(id => id == invalidId)))
                .Returns(taxes.LastOrDefault(t => t.Name == "No such name"));

            taxesServices
                .Setup(t => t.GetByCommunityId(It.Is<int>(id => id == validId)))
                .Returns(taxes);

            return taxesServices.Object;
        }

        public static ICommunitiesService GetCommunitiesService()
        {
            var communitiesService = new Mock<ICommunitiesService>();

            communitiesService
                .Setup(c => c.HasUser(It.Is<int>(id => id == validId), It.IsAny<string>()))
                .Returns(true);

            communitiesService
                .Setup(c => c.HasUser(It.Is<int>(id => id == invalidId), It.IsAny<string>()))
                .Returns(false);

            return communitiesService.Object;
        }
    }
}
