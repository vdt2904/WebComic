using WebComic.Models;

namespace WebComic.Interface
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<Genre> GetGenreById(int id);
        Task UpdateGenre(int id, Genre genre);
        Task<Genre> CreateGenre(Genre genre);
        Task DeleteGenre(int id);
    }
}
