using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebComic.Helpter;
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
        private readonly Reuse _reuse;
        public GenresController(ComicDbContext context, IGenreRepository genreRepository, IDistributedCache distributedCache, Reuse reuse)
        {
            _context = context;
            _genresRepository = genreRepository;
            _distributedCache = distributedCache;
            _reuse = reuse;
        }

		// GET: api/Genres
		[HttpGet]
		[Route("get/{page}")]
		public async Task<ActionResult<IEnumerable<Genre>>> GetGenres(int page)
		{
			try
			{
                int pageSize = 10;
				var genres = new List<Genre>();
				var cachedGenres = await _distributedCache.GetStringAsync("genresList");
				if (cachedGenres != null)
				{
					genres = JsonConvert.DeserializeObject<List<Genre>>(cachedGenres);
				}
				else
				{
					var genresEnumerable = await _genresRepository.GetGenres();
					genres = genresEnumerable.ToList();
					await _reuse.ReuseCURD(genres, "genresList");
				}

				var paginatedGenres = genres
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				var totalRecords = genres.Count;

				var response = new
				{
					Data = paginatedGenres,
					PageNumber = page,
					TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
				};

				return Ok(response);
			}
			catch (Exception ex)
			{
				// Log exception
				return StatusCode(500, "Internal server error");
			}
		}
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            try
            {
                var genre = await _genresRepository.GetGenreById(id);
                return Ok(genre);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            try 
            {
                await _genresRepository.UpdateGenre(id, genre);
                var genres = await _genresRepository.GetGenres();
                await _reuse.ReuseCURD(genres, "genresList");
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            try
            {
                await _genresRepository.CreateGenre(genre);
                var genres = await _genresRepository.GetGenres();
                await _reuse.ReuseCURD(genres, "genresList");
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                await _genresRepository.DeleteGenre(id);
                var genres = await _genresRepository.GetGenres();
                await _reuse.ReuseCURD(genres, "genresList");
                return Ok("Xóa thành công");
            }
            catch (InvalidOperationException ex) {
                return BadRequest(ex.Message);
            }
           
        }

    }
}
