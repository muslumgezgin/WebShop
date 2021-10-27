using System;

namespace WepShop.Application.Behaviours
{
    public class BadRequestException :Exception
    {
        public BadRequestException(string message) 
            :base(message)
        {
            
        }
        
    }
}