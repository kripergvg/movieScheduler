using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Application.Infrastructure
{
    public abstract class BaseService
    {
        protected BaseService(IUnitOfWorkFactory unitOfWorkFactory, IValidationDictionary validationDictionary)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            ValidationDictionary = validationDictionary;
        }

        protected IUnitOfWorkFactory UnitOfWorkFactory { get; set; }
        protected IValidationDictionary ValidationDictionary { get; set; }
    }
}
