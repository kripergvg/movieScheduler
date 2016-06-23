using MovieSheduler.Domain.Infrastructure;

namespace MovieSheduler.Domain.Movie
{
    public class Movie : Entity
    {
        /// <summary>
        /// Movie name
        /// </summary>
        public string Name { get; set; }
    }
}
