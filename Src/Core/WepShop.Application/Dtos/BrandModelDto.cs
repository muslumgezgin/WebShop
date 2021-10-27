using System;
using WepShop.Domain.Common;

namespace WepShop.Application.Dtos
{
    public class BrandModelDto : BaseEntity
    {
        public Guid brandId { get; set; }
        public string modelName { get; set; }
        public string decription { get; set; }
        public BrandDto brand { get; set; }


        public BrandModelDto()
        {
            
        }

        public BrandModelDto(Guid id,Guid brandId, string modelName, string decription)
        {
            this.Id = id;
            this.brandId = brandId;
            this.modelName = modelName;
            this.decription = decription;
        }
    }
}