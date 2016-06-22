using System.Linq;
using System.Web.Mvc;

namespace MovieSheduler.Presentation.Core.Messager
{
    public class NotifierFilterAttribute : ActionFilterAttribute
    {
        private readonly INotifier _notifier;

        public NotifierFilterAttribute(INotifier notifier)
        {
            _notifier = notifier;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var messages = _notifier.Messages;
            if (messages.Any())
            {
                filterContext.Controller.TempData[Constants.TEMP_DATA_KEY] = messages;
            }
        }
    }
}