using System;
using WepShop.Domain.Common;

namespace WepShop.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public Guid supplierId { get; set; }
        
        public Guid brandModelId { get; set; }
        
        public string  productCode { get; set; }
        
        public string productName { get; set; }
        
        public string description { get; set; }
        
        public double price { get; set; }
        
        public int quantity { get; set; }
        
    }
}