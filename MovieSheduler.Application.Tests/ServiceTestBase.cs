using Moq;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Application.Tests
{
    public abstract class ServiceTestBase
    {
        protected ServiceTestBase()
        {           
            UnitOfWorkMock = new Mock<IUnitOfWork>();

            UnitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
            UnitOfWorkFactoryMock.Setup(f => f.Create()).Returns(UnitOfWorkMock.Object);

            ValidationDictionaryMock = new Mock<IValidationDictionary>();

            //Initialize automapper
            DtoMappings.CreateMap();
        }
        
        protected Mock<IUnitOfWorkFactory> UnitOfWorkFactoryMock { get; }
        protected Mock<IUnitOfWork> UnitOfWorkMock { get; }
        protected Mock<IValidationDictionary> ValidationDictionaryMock { get; }
    }
}
