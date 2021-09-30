using System;
using System.Threading.Tasks;
using DddNoDuplicates.Domain;
using DddNoDuplicates.Infrastructure;
using DddNoDuplicates.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DddNoDuplicates.Tests
{
    public class UpdateProductNameUsingRequirementsHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepository = new();

        private UpdateProductNameUsingRequirementsHandler CreateHandler()
        {
            return new(_mockRepository.Object);
        }
        
        [Fact]
        public async Task Not_update_name_when_name_is_not_unique()
        {
            var productId = Guid.NewGuid();
            
            _mockRepository
                .Setup(x => x.GetById(productId))
                .ReturnsAsync(new Product("foo", true));
            
            _mockRepository
                .Setup(x => x.IsNameUnique("bar"))
                .ReturnsAsync(false);
            
            var sut = CreateHandler();

            var result = await sut.Handle(new UpdateProductName {Id = productId, Name = "bar"});

            result.IsSuccess.Should().BeFalse();
        }
        
        [Fact]
        public async Task Update_name_when_name_is_not_unique()
        {
            var productId = Guid.NewGuid();
            
            _mockRepository
                .Setup(x => x.GetById(productId))
                .ReturnsAsync(new Product("foo", true));
            
            _mockRepository
                .Setup(x => x.IsNameUnique("bar"))
                .ReturnsAsync(true);
            
            var sut = CreateHandler();

            var result = await sut.Handle(new UpdateProductName {Id = productId, Name = "bar"});

            result.IsSuccess.Should().BeTrue();
        }
    }
}
