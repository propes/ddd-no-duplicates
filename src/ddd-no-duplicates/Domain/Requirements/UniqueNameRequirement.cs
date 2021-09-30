using System;

namespace DddNoDuplicates.Domain.Requirements
{
    public class UniqueNameRequirement
    {
        public bool IsSatisfied { get; }

        public UniqueNameRequirement(Func<bool> getIsUniqueName)
        {
            IsSatisfied = getIsUniqueName();
        }
    }
}