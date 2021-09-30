using System;
using System.Threading.Tasks;
using DddNoDuplicates.Domain;

namespace DddNoDuplicates.Infrastructure
{
    public interface IProductRepository
    {
        Task<Product> GetById(Guid id);
        Task<bool> IsNameUnique(string name);
        Task Update(Product product);
    }
}