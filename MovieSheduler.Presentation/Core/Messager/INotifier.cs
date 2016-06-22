using System.Collections.Generic;

namespace MovieSheduler.Presentation.Core.Messager
{
    public interface INotifier
    {
        IList<Message> Messages { get; }
        void AddMessage(MessageType severity, string text, params object[] format);
    }
}