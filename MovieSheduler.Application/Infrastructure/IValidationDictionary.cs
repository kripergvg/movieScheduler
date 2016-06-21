namespace MovieSheduler.Application.Infrastructure
{
    public interface IValidationDictionary
    {
        void AddError(string errorMessage);
        bool IsValid { get; }
    }
}
