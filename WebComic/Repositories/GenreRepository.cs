using Microsoft.EntityFrameworkCore;
using WebComic.Interface;
using WebComic.Models;

namespace WebComic.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ComicDbContext _context;

        public GenreRepository(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var a = await _context.Genres.ToListAsync();
            return a;
        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task UpdateGenre(int id, Genre genre)
        {
            if (id != genre.GenreId)
            {
                throw new ArgumentException("ID mismatch");
            }

            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            // Kiểm tra xem tên thể loại đã tồn tại chưa
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genre.Name);

            if (existingGenre != null)
            {
                // Nếu tên thể loại đã tồn tại, bạn có thể xử lý tùy ý, ví dụ như trả về BadRequest
                throw new InvalidOperationException("Tên thể loại đã tồn tại trong cơ sở dữ liệu.");
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }


        public async Task DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}

