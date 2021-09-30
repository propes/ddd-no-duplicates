using System.Threading.Tasks;
using DddNoDuplicates.Domain;
using DddNoDuplicates.Domain.Requirements;
using DddNoDuplicates.Infrastructure;

namespace DddNoDuplicates.Services
{
    public class UpdateProductNameHandler
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<Result> Handle(UpdateProductName command)
        {
            var product = await _productRepository.GetById(command.Id);

            var isNameUnique = await _productRepository.IsNameUnique(command.Name);
            
            var result = product.UpdateName(command.Name, isNameUnique);

            if (!result.IsSuccess)
            {
                return result;
            }

            await _productRepository.Update(product);
            
            return Result.Success();
        }
    }
}