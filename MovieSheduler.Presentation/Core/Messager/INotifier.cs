using System.Collections.Generic;

namespace MovieSheduler.Presentation.Core.Messager
{
    public interface INotifier
    {
        /// <summary>
        /// Current messages
        /// </summary>
        IList<IMessage> Messages { get; }

        /// <summary>
        /// Add message
        /// </summary>
        /// <param name="severity">Severity of message</param>
        /// <param name="text">Text of message</param>
        void AddMessage(MessageType severity, string text);
    }
}