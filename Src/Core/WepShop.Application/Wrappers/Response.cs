using System.Collections;
using System.Collections.Generic;

namespace WepShop.Application.Wrappers
{
    public interface IResponse
    {
        bool Succeeded { get; set; }
        
        string FailureType { get; set; }
        
        IDictionary <string,string[]> Failures { get; }
    }
    
    public class Response<T> :IResponse
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string FailureType { get; set; }
        public IDictionary<string, string[]> Failures { get; }

        public Response()
        {
            
        }

        public Response(T data)
        {
            Succeeded = true;
            Data = data;
        }

        public Response( string failureType, IDictionary<string, string[]> failures)
        {
            Data = default(T);
            Succeeded = false;
            FailureType = failureType;
            Failures = failures;
        }
        
    }
    public class Response : IResponse
    {
        public bool Succeeded { get; set; } = false;

        public string FailureType { get; set; }
        public IDictionary<string, string[]> Failures { get; }

        public Response(bool success)
        {
            this.Succeeded = success;
        }

        public Response(string failureType, IDictionary<string, string[]> failures)
        {
            this.Succeeded = false;
            this.Failures = failures;
            this.FailureType = failureType;
        }
    }
}