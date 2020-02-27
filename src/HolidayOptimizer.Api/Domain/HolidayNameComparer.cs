using HolidayOptimizer.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HolidayOptimizer.Api.Domain
{
    public class HolidayNameComparer : IEqualityComparer<Holiday>
    {
        public bool Equals([AllowNull] Holiday x, [AllowNull] Holiday y)
        {
            if (x == null) return false;
            if (y == null) return false;

            return x.Name.Equals(y.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode([DisallowNull] Holiday obj)
        {
            int hashCode = obj.Name.GetHashCode();

            return hashCode;
        }
    }
}
