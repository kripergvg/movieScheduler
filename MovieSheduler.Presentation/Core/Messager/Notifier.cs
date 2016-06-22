using System.Collections.Generic;

namespace MovieSheduler.Presentation.Core.Messager
{
    public class Notifier : INotifier
    {
        public IList<Message> Messages { get; } = new List<Message>();

        public void AddMessage(MessageType severity, string text, params object[] format)
        {
            Messages.Add(new Message { Severity = severity, Text = string.Format(text, format) });
        }
    }
}