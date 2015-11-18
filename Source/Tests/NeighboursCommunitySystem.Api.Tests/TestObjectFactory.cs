namespace NeighboursCommunitySystem.Api.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using Models;
    using Server.DataTransferModels.Taxes;
    using Services.Data.Contracts;

    internal class TestObjectFactory
    {
        public const int validId = 1;
        public const int invalidId = 2;

        private static readonly IQueryable<Tax> taxes = new List<Tax>
        {
            new Tax
            {
                Name = "Test Tax",
                Price = 123,
                Description = "Test description"
            },
            null
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
                .Setup(t => t.GetById(It.Is<int>(id => id == invalidId)))
                .Returns(taxes.Last());

            return taxesServices.Object;
        }

        public static ICommunitiesService GetCommunitiesService()
        {
            var communitiesServices = new Mock<ICommunitiesService>();
            communitiesServices
                .Setup(c => c.HasUser(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true);

            return communitiesServices.Object;
        }
    }
}
