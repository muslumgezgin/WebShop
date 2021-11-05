using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace WebShop.Infrastructure.Context
{
    public class GuidValueGenerator : ValueGenerator<Guid>
    {
        public GuidValueGenerator()
        {

        }

        public override bool GeneratesTemporaryValues => false;

        public override Guid Next(EntityEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            Guid res = Guid.NewGuid();
            return res;
        }
    }
}