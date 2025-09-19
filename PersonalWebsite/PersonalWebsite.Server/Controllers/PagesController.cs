using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Server.Data;
using PersonalWebsite.Server.Models;

namespace PersonalWebsite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            return await _context.Pages
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Order)
                .ToListAsync();
        }

        // GET: api/pages/all (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Page>>> GetAllPages()
        {
            return await _context.Pages
                .OrderBy(p => p.Order)
                .ToListAsync();
        }

        // GET: api/pages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPage(int id)
        {
            var page = await _context.Pages.FindAsync(id);

            if (page == null)
            {
                return NotFound();
            }

            // If page is not published and user is not admin, return not found
            if (!page.IsPublished && !User.IsInRole("Admin"))
            {
                return NotFound();
            }

            return page;
        }

        // GET: api/pages/slug/home
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<Page>> GetPageBySlug(string slug)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.Slug == slug);

            if (page == null)
            {
                return NotFound();
            }

            // If page is not published and user is not admin, return not found
            if (!page.IsPublished && !User.IsInRole("Admin"))
            {
                return NotFound();
            }

            return page;
        }

        // POST: api/pages (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Page>> CreatePage(Page page)
        {
            // Ensure slug is unique
            if (await _context.Pages.AnyAsync(p => p.Slug == page.Slug))
            {
                return BadRequest(new { message = "A page with this slug already exists" });
            }

            page.CreatedAt = DateTime.UtcNow;
            
            if (page.IsPublished)
            {
                page.PublishedAt = DateTime.UtcNow;
            }

            _context.Pages.Add(page);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPage), new { id = page.Id }, page);
        }

        // PUT: api/pages/5 (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePage(int id, Page page)
        {
            if (id != page.Id)
            {
                return BadRequest();
            }

            // Check if slug is unique (except for this page)
            if (await _context.Pages.AnyAsync(p => p.Slug == page.Slug && p.Id != id))
            {
                return BadRequest(new { message = "A page with this slug already exists" });
            }

            var existingPage = await _context.Pages.FindAsync(id);
            if (existingPage == null)
            {
                return NotFound();
            }

            // Update page properties
            existingPage.Title = page.Title;
            existingPage.Slug = page.Slug;
            existingPage.Description = page.Description;
            existingPage.Content = page.Content;
            existingPage.Order = page.Order;
            existingPage.ShowInNavigation = page.ShowInNavigation;
            existingPage.LayoutJson = page.LayoutJson;
            existingPage.UpdatedAt = DateTime.UtcNow;

            // Handle publishing status changes
            if (!existingPage.IsPublished && page.IsPublished)
            {
                existingPage.IsPublished = true;
                existingPage.PublishedAt = DateTime.UtcNow;
            }
            else if (existingPage.IsPublished && !page.IsPublished)
            {
                existingPage.IsPublished = false;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(id))
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

        // DELETE: api/pages/5 (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/pages/navigation
        [HttpGet("navigation")]
        public async Task<ActionResult<IEnumerable<object>>> GetNavigation()
        {
            var pages = await _context.Pages
                .Where(p => p.IsPublished && p.ShowInNavigation)
                .OrderBy(p => p.Order)
                .Select(p => new { p.Id, p.Title, p.Slug })
                .ToListAsync();

            return Ok(pages);
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}