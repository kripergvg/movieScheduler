namespace MovieSheduler.Presentation.Core.Messager
{
    public interface IMessage
    {
        MessageType Severity { get; set; }
        string Text { get; set; }
        string Generate();
    }
}
