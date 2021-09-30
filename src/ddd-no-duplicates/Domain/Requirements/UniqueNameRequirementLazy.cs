using System;
using System.Threading.Tasks;

namespace DddNoDuplicates.Domain.Requirements
{
    public class UniqueNameRequirementLazy
    {
        private readonly Func<Task<bool>> _getIsUniqueName;

        public Task<bool> IsSatisfied => _getIsUniqueName();

        public UniqueNameRequirementLazy(Func<Task<bool>> getIsUniqueName)
        {
            _getIsUniqueName = getIsUniqueName;
        }
    }
}