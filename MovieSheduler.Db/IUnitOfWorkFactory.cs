namespace MovieSheduler.Db
{
    interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
