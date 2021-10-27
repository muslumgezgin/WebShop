using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace WepShop.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() 
            : base("one or more validation have occurred ")
        {
            Failures = new Dictionary<string, string[]>();

        }

        public ValidationException(List<ValidationFailure> failures) 
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => $"{e.ErrorCode} : {e.ErrorMessage}")
                    .Distinct()
                    .ToArray();
                Failures.Add(propertyName,propertyFailures);
            }
        }
        public IDictionary<string, string[]> Failures { get; }

    }
}