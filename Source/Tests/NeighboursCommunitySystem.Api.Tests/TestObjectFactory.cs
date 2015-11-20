namespace NeighboursCommunitySystem.Api.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Moq;
    using Services.Data.Contracts;
    using Microsoft.AspNet.Identity;
    using Server.DataTransferModels.Taxes;
    using System;

    internal class TestObjectFactory
    {
        internal const int validId = 1;
        internal const int validTaxIdInvalidCommunity = 2;
        internal const int invalidId = 3;

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

        private static readonly IQueryable<Community> communities = new List<Community>
        {
            new Community
            {
                Name = "Test Community 1",
                Description = "Test description",
                Users = new List<User> {new User { Id = "TestUserId"} }
            },
            new Community
            {
                Name = "Test Community 2",
                Description = "Test description"
            }
        }.AsQueryable();

        internal static List<Tax> Taxes
        {
            get
            {
                return taxes.ToList();
            }
        }

        internal static List<Community> Communities
        {
            get
            {
                return communities.ToList();
            }
        }

        internal static ITaxesService GetTaxesService()
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

        internal static ICommunitiesService GetCommunitiesService()
        {
            var communitiesService = new Mock<ICommunitiesService>();

            communitiesService
                .Setup(c => c.HasUser(It.Is<int>(id => id == validId), It.IsAny<string>()))
                .Returns(true);

            communitiesService
                .Setup(c => c.HasUser(It.Is<int>(id => id == invalidId), It.IsAny<string>()))
                .Returns(false);

            communitiesService
                .Setup(c => c.All())
                .Returns(communities);

            return communitiesService.Object;
        }

        internal static TaxRequestTransferModel GetValidTaxRequestModel()
        {
            return new TaxRequestTransferModel
            {
                Name = "Valid model",
                Deadline = DateTime.Now.AddDays(10),
                Price = 123,
                CommunityId = validId
            };
        }

        internal static TaxRequestTransferModel GetValidTaxRequestModelWithInvalidCommunity()
        {
            return new TaxRequestTransferModel
            {
                Name = "Valid model",
                Deadline = DateTime.Now.AddDays(10),
                Price = 123,
                CommunityId = invalidId
            };
        }

        internal static TaxRequestTransferModel GetInvalidTaxRequestModel()
        {
            return new TaxRequestTransferModel
            {
                Name = String.Empty,
                Deadline = DateTime.Now.AddDays(10),
                Price = 123,
                CommunityId = validId
            };
        }
    }
}
