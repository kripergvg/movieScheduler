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
            _errors.Add(key, errorMessage);
        }

        public IDictionary<string, string> Errors => _errors;
    }
}
