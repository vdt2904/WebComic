using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebComic.Interface;
using WebComic.Models;

namespace WebComic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ComicDbContext _context;
        private readonly IGenreRepository _genresRepository;
        private readonly IDistributedCache _distributedCache;
        public GenresController(ComicDbContext context, IGenreRepository genreRepository, IDistributedCache distributedCache)
        {
            _context = context;
            _genresRepository = genreRepository;
            _distributedCache = distributedCache;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var genres = new List<Genre>();
            var cachedGenres = await _distributedCache.GetStringAsync("GenresList");
            if (cachedGenres != null)
            {
                genres = JsonConvert.DeserializeObject<List<Genre>>(cachedGenres);
            }
            else
            {
                var genresEnumerable = await _genresRepository.GetGenres();
                genres = genresEnumerable.ToList();
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };
                await _distributedCache.SetStringAsync("GenresList", JsonConvert.SerializeObject(genres), cacheOptions);

            }
            return Ok(genres);
        }


        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            return Ok(_genresRepository.GetGenreById(id));
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            await _genresRepository.UpdateGenre(id, genre);
            var genres = await _genresRepository.GetGenres();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
            await _distributedCache.SetStringAsync("GenresList", JsonConvert.SerializeObject(genres), cacheOptions);
            return Ok();
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            await _genresRepository.CreateGenre(genre);
            var genres = await _genresRepository.GetGenres();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
            await _distributedCache.SetStringAsync("GenresList", JsonConvert.SerializeObject(genres), cacheOptions);
            return Ok();
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genresRepository.DeleteGenre(id);
            var genres = await _genresRepository.GetGenres();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
            await _distributedCache.SetStringAsync("GenresList", JsonConvert.SerializeObject(genres), cacheOptions);
            return Ok();
        }

    }
}
