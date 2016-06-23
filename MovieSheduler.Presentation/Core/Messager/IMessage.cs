namespace MovieSheduler.Presentation.Core.Messager
{
    public interface IMessage
    {
        /// <summary>
        /// Severity of message
        /// </summary>
        MessageType Severity { get; set; }

        /// <summary>
        /// Text of message
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Generate message
        /// </summary>
        /// <returns>String representation of message</returns>
        string Generate();
    }
}
