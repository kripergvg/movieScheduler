using System.Collections.Generic;

namespace MovieSheduler.Application.Infrastructure
{
    public interface IValidationDictionary
    {
        /// <summary>
        /// Error adding
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="errorMessage">Error message</param>
        void AddError(string key, string errorMessage);

        /// <summary>
        /// Return true if validation doesnt have errors
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Errors collection
        /// </summary>
        IDictionary<string, string> Errors { get; }
    }
}
