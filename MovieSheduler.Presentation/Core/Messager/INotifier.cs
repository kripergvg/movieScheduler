using System.Collections.Generic;

namespace MovieSheduler.Presentation.Core.Messager
{
    public interface INotifier
    {
        IList<IMessage> Messages { get; }
        void AddMessage(MessageType severity, string text, params object[] format);
    }
}