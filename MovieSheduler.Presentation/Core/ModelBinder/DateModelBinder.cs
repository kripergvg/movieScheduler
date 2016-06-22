using System;
using System.Web.Mvc;

namespace MovieSheduler.Presentation.Core.ModelBinder
{
    public class DateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            //TODO ПЕРЕДЕЛАТь
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult != null)
            {
                return Convert.ToDateTime(valueResult.AttemptedValue);
                
            }
            return null;
        }
    }
}