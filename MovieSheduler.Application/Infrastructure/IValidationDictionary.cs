using System.Collections.Generic;

namespace MovieSheduler.Application.Infrastructure
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
        IDictionary<string, string> Errors { get; }
    }
}
