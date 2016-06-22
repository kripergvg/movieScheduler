using System.Linq;
using System.Web.Mvc;
using Moq;
using MovieSheduler.Presentation.Core.Messager;
using Shouldly;
using Xunit;

namespace MovieSheduler.Presentation.Tests
{
    public class NotifierFilterAttributeTests
    {
        [Fact]
        public void OnActionExecuted_Should_Add_TempData()
        {
            //Arrange     
            var notifer = Mock.Of<INotifier>(n => n.Messages == new[] {new Message(MessageType.Info, "TestMessage")});
            var notifierAttribute = new NotifierFilterAttribute(notifer);

            var tempDataDictionary = new TempDataDictionary();
            var actionContext = Mock.Of<ActionExecutedContext>(c => c.Controller.TempData == tempDataDictionary);

            //Act
            notifierAttribute.OnActionExecuted(actionContext);

            //Assert            
            tempDataDictionary.ShouldContain(
                d => d.Key == Constants.TEMP_DATA_KEY && ((IMessage[]) d.Value).Any(m => m.Severity == MessageType.Info && m.Text == "TestMessage"));
        }

        [Fact]
        public void OnActionExecuted_Should_Be_Empty_TempData_Messages()
        {
            //Arrange     
            var notifer = Mock.Of<INotifier>(n => n.Messages == new Message[0]);
            var notifierAttribute = new NotifierFilterAttribute(notifer);

            var tempDataDictionary = new TempDataDictionary();
            var actionContext = Mock.Of<ActionExecutedContext>(c => c.Controller.TempData == tempDataDictionary);

            //Act
            notifierAttribute.OnActionExecuted(actionContext);

            //Assert            
            tempDataDictionary.ShouldNotContain( d => d.Key == Constants.TEMP_DATA_KEY);
        }
    }
}
