using MovieSheduler.Presentation.Core.Messager;
using Shouldly;
using Xunit;

namespace MovieSheduler.Presentation.Tests
{
    public class NotifierTests
    {
        [Fact]
        public void AddMessage_Should_Add_In_Messages()
        {
            //Arrange     
            var notifier=new Notifier();

            //Act
            notifier.AddMessage(MessageType.Info, "Test");

            //Assert            
            notifier.Messages.ShouldContain(m => m.Text == "Test" && m.Severity == MessageType.Info);
        }

        [Fact]
        public void AddMessage_Should_Empty_Collection()
        {
            //Arrange     

            //Act
            var notifier = new Notifier();

            //Assert            
            notifier.Messages.ShouldBeEmpty();
        }
    }
}
