using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebComic.Models;
using WebComic.ViewModel;

namespace WebComic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ComicDbContext _context;

        public TagsController(ComicDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags(int pageNumber = 1)
        {
            try
            {
                int pageSize = 5;
                if (pageNumber < 1)
                {
                    return BadRequest("Invalid page number, should be greater than 0.");
                }

                var totalRecords = await _context.Tags.CountAsync();
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                if (pageNumber > totalPages && totalPages > 0)
                {
                    return BadRequest($"Invalid page number, should be less than or equal to {totalPages}.");
                }

                var tags = await _context.Tags
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

                var tagDtos = tags.Select(tag => new TagViewModel
                {
                    TagId = tag.TagId,
                    Name = tag.Name
                }).ToList();

                var metadata = new
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber,
                    PageSize = pageSize
                };

                Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(metadata));

                return Ok(tagDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            if (_context.Tags == null)
            {
                return NotFound();
            }
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.TagId)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            try
            {
                // Kiểm tra xem tag đã tồn tại chưa
                var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Name == tag.Name);
                if (existingTag != null)
                {
                    ModelState.AddModelError("TagName", "Tag name already exists.");
                    return BadRequest(ModelState);
                }

                // Thêm tag vào cơ sở dữ liệu
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTag", new { id = tag.TagId }, tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (_context.Tags == null)
            {
                return NotFound();
            }
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            var check = _context.ComicTags.FirstOrDefault(x => x.TagId == id);
            if (check != null)
            {
                return BadRequest();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return (_context.Tags?.Any(e => e.TagId == id)).GetValueOrDefault();
        }
    }
}
