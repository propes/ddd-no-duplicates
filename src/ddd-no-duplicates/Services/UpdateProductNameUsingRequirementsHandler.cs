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

            // Domain logic leaks here into this repository method.
            var uniqueNameRequirement = new UniqueNameRequirementLazy(() => _productRepository.IsNameUnique(command.Name));
            
            // However, the outcome of the business rule is a required input to the domain operation
            // so the domain object can't be put in an invalid state.
            var result = await product.UpdateName(command.Name, uniqueNameRequirement);

            if (!result.IsSuccess)
            {
                return result;
            }

            try
            {
                await _productRepository.Update(product);
            }
            // Assume the repository will throw an exception in the situation of a race condition.
            catch (DbUpdateException ex)
            {
                if (ex.Message.Contains("IX_Product_Name"))
                    return Result.Fail(ExceptionMessages.ProductNameMustBeUnique);

                throw;
            }

            return Result.Success();
        }
    }
}