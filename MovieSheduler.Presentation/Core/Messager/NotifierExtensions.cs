using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace MovieSheduler.Presentation.Core.Messager
{
    public static class NotifierExtensions
    {
        public static void Error(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageType.Danger, text, format);
        }

        public static void Info(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageType.Info, text, format);
        }

        public static void Success(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageType.Success, text, format);
        }

        public static void Warning(this INotifier notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageType.Warning, text, format);
        }

        public static MvcHtmlString DisplayMessages(this ViewContext context)
        {
            if (!context.Controller.TempData.ContainsKey(Constants.TEMP_DATA_KEY))
            {
                return null;
            }

            var messages = (IEnumerable<Message>)context.Controller.TempData[Constants.TEMP_DATA_KEY];
            var builder = new StringBuilder();
            foreach (var message in messages)
            {
                builder.AppendLine(message.Generate());
            }

            return builder.ToHtmlString();
        }

        private static MvcHtmlString ToHtmlString(this StringBuilder input)
        {
            return MvcHtmlString.Create(input.ToString());
        }
    }
}