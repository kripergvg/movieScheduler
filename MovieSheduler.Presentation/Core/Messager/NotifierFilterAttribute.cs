using System.Linq;
using System.Web.Mvc;

namespace MovieSheduler.Presentation.Core.Messager
{
    public class NotifierFilterAttribute : ActionFilterAttribute
    {
        public NotifierFilterAttribute(INotifier notifier)
        {
            Notifier = notifier;
        }

        public INotifier Notifier { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var messages = Notifier.Messages;
            if (messages.Any())
            {
                filterContext.Controller.TempData[Constants.TEMP_DATA_KEY] = messages;
            }
        }
    }
}