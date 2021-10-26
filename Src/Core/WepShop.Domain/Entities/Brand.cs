using System;
using WepShop.Domain.Common;

namespace WepShop.Domain.Entities
{
    public class Brand :AuditableEntity
    {
        public string brandName { get; set; }
        public string description { get; set; }

        public Brand()
        {
            
        }

        public Brand(Guid id,string brandName, string description)
        {
            this.Id = id;
            this.brandName = brandName;
            this.description = description;
        }
    }
}