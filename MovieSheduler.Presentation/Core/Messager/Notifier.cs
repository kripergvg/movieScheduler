using System.Collections.Generic;

namespace MovieSheduler.Presentation.Core.Messager
{
    public class Notifier : INotifier
    {
        public IList<IMessage> Messages { get; } = new List<IMessage>();

        public void AddMessage(MessageType severity, string text)
        {
            Messages.Add(new Message(severity, text));
        }
    }
}