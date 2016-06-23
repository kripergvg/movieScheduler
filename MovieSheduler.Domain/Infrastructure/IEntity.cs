namespace MovieSheduler.Domain.Infrastructure
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
