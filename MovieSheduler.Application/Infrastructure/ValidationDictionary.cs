using System.Collections.Generic;
using System.Linq;

namespace MovieSheduler.Application.Infrastructure
{
    public class ValidationDictionary : IValidationDictionary
    {
        private readonly List<string> _errors; 

        public ValidationDictionary()
        {
            _errors = new List<string>();
        }
        public void AddError(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        public bool IsValid => !_errors.Any();
    }
}
