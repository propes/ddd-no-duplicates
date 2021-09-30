using System;
using System.Threading.Tasks;
using DddNoDuplicates.Domain.Requirements;

namespace DddNoDuplicates.Domain
{
    public class Product
    {
        public Guid Id { get; }
        
        public string Name { get; private set; }

        public Product(string name, bool isUnique)
        {
            Id = Guid.NewGuid();
            UpdateName(name, isUnique);
        }

        public Result UpdateName(string name, bool isNameUnique)
        {
            if (!isNameUnique)
            {
                return Result.Fail("Name must be unique.");
            }

            Name = name;
            return Result.Success();
        }

        public async Task<Result> UpdateName(string name, UniqueNameRequirementLazy uniqueNameRequirement)
        {
            if (!await uniqueNameRequirement.IsSatisfied)
            {
                return Result.Fail("Name must be unique.");
            }
            
            Name = name;
            return Result.Success();
        }
    }
}
