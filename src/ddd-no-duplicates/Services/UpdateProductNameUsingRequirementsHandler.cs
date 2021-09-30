using System.Threading.Tasks;
using DddNoDuplicates.Domain;
using DddNoDuplicates.Domain.Requirements;
using DddNoDuplicates.Infrastructure;

namespace DddNoDuplicates.Services
{
    public class UpdateProductNameUsingRequirementsHandler
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductNameUsingRequirementsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(UpdateProductName command)
        {
            var product = await _productRepository.GetById(command.Id);

            var uniqueNameRequirement = new UniqueNameRequirementLazy(() => _productRepository.IsNameUnique(command.Name));
            
            var result = await product.UpdateName(command.Name, uniqueNameRequirement);

            if (!result.IsSuccess)
            {
                return result;
            }

            await _productRepository.Update(product);
            
            return Result.Success();
        }
    }
}