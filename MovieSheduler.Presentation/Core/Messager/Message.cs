using System.Text;
using System.Web.Mvc;

namespace MovieSheduler.Presentation.Core.Messager
{
    public class Message: IMessage
    {
        public Message(MessageType severity, string text)
        {
            Severity = severity;
            Text = text;
        }

        public MessageType Severity { get; set; }

        public string Text { get; set; }

        public string Generate()
        {           
            if (Severity == MessageType.None)
            {
                Severity = MessageType.Info;
            }
            var sb = new StringBuilder();
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("alert fade in");
            divTag.AddCssClass("alert-" + Severity.ToString().ToLower());


            var spanTag = new TagBuilder("span");
            spanTag.MergeAttribute("id", "MessageContent");

            var isDismissable = Severity != MessageType.Danger;

            if (isDismissable)
            {
                divTag.AddCssClass("alert-dismissable");
            }

            sb.Append(divTag.ToString(TagRenderMode.StartTag));

            if (isDismissable)
            {
                var buttonTag = new TagBuilder("button");
                buttonTag.MergeAttribute("class", "close");
                buttonTag.MergeAttribute("data-dismiss", "alert");
                buttonTag.MergeAttribute("aria-hidden", "true");
                buttonTag.InnerHtml = "×";
                sb.Append(buttonTag.ToString(TagRenderMode.Normal));
            }

            sb.Append(spanTag.ToString(TagRenderMode.StartTag));
            sb.Append(Text);
            sb.Append(spanTag.ToString(TagRenderMode.EndTag));
            sb.Append(divTag.ToString(TagRenderMode.EndTag));

            return sb.ToString();
        }
    }
}