using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieSheduler.Application.Infrastructure
{
    public class ValidationDictionary : IValidationDictionary
    {
        private readonly IDictionary<string,string> _errors;

        public ValidationDictionary()
        {
            _errors =new Dictionary<string, string>();
        }

        public bool IsValid => !_errors.Any();

        public void AddError(string key, string errorMessage)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentException("Parametr cant be null or empty", nameof(key));

            if (String.IsNullOrEmpty(errorMessage))
                throw new ArgumentException("Parametr cant be null or empty", nameof(key));

            _errors.Add(key, errorMessage);
        }

        public IDictionary<string, string> Errors => _errors;
    }
}
