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
            Genre result = await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == id);
            if(result == null)
            {
                throw new InvalidOperationException("Thể loại không tồn tại");
            }
            return result;
        }

        public async Task UpdateGenre(int id, Genre genre)
        {
            if (id != genre.GenreId)
            {
                throw new InvalidOperationException("ID không trngf khớp");
            }
            Genre g = _context.Genres.FirstOrDefault(g => g.Name == genre.Name && g.GenreId != genre.GenreId);
            if (g != null)
            {
                throw new InvalidOperationException("Thể loại đã tồn tại");
            }
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            if(genre.Name == null || genre.Name == "")
            {
                throw new InvalidOperationException("Không được để trống!");
            }
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
                List<ComicGenre> comicgenre =  _context.ComicGenres.Where(i => i.GenreId == id).ToList();
                if (comicgenre.Count() > 0)
                {
                    throw new InvalidOperationException("Không thể xóa thể loại này");
                }
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
            else
            {

            throw new InvalidOperationException("Thể loại dã được xóa hoặc không tồn tại!");
            }
        }
    }
}

