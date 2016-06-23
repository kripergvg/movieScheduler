namespace MovieSheduler.Domain.Infrastructure
{
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Create unit of work
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create();
    }
}
