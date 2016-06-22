using System.Web.Mvc;
using MovieSheduler.Application.Infrastructure;

namespace MovieSheduler.Presentation.Core
{
    public static class ModelStateExtensions
    {
        public static void AddErrorsFromValidationDictionary(this ModelStateDictionary modelStateDictionary, IValidationDictionary validationDictionary)
        {
            foreach (var error in validationDictionary.Errors)
            {
                modelStateDictionary.AddModelError(error.Key, error.Value);
            }          
        }
    }
}