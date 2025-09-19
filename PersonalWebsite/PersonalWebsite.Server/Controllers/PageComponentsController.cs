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
    public class PageComponentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PageComponentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/pagecomponents/page/5
        [HttpGet("page/{pageId}")]
        public async Task<ActionResult<IEnumerable<PageComponent>>> GetPageComponents(int pageId)
        {
            var page = await _context.Pages.FindAsync(pageId);
            if (page == null)
            {
                return NotFound();
            }

            // If page is not published and user is not admin, return not found
            if (!page.IsPublished && !User.IsInRole("Admin"))
            {
                return NotFound();
            }

            return await _context.PageComponents
                .Where(pc => pc.PageId == pageId)
                .OrderBy(pc => pc.Order)
                .ToListAsync();
        }

        // GET: api/pagecomponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageComponent>> GetPageComponent(int id)
        {
            var pageComponent = await _context.PageComponents.FindAsync(id);

            if (pageComponent == null)
            {
                return NotFound();
            }

            // Check if the component belongs to a published page or if user is admin
            var page = await _context.Pages.FindAsync(pageComponent.PageId);
            if (page == null || (!page.IsPublished && !User.IsInRole("Admin")))
            {
                return NotFound();
            }

            return pageComponent;
        }

        // POST: api/pagecomponents (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<PageComponent>> CreatePageComponent(PageComponent pageComponent)
        {
            // Verify the page exists
            var page = await _context.Pages.FindAsync(pageComponent.PageId);
            if (page == null)
            {
                return BadRequest(new { message = "The specified page does not exist" });
            }

            _context.PageComponents.Add(pageComponent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPageComponent), new { id = pageComponent.Id }, pageComponent);
        }

        // PUT: api/pagecomponents/5 (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePageComponent(int id, PageComponent pageComponent)
        {
            if (id != pageComponent.Id)
            {
                return BadRequest();
            }

            var existingComponent = await _context.PageComponents.FindAsync(id);
            if (existingComponent == null)
            {
                return NotFound();
            }

            // Update component properties
            existingComponent.ComponentType = pageComponent.ComponentType;
            existingComponent.Content = pageComponent.Content;
            existingComponent.StyleJson = pageComponent.StyleJson;
            existingComponent.PropertiesJson = pageComponent.PropertiesJson;
            existingComponent.Order = pageComponent.Order;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageComponentExists(id))
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

        // DELETE: api/pagecomponents/5 (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePageComponent(int id)
        {
            var pageComponent = await _context.PageComponents.FindAsync(id);
            if (pageComponent == null)
            {
                return NotFound();
            }

            _context.PageComponents.Remove(pageComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/pagecomponents/reorder (admin only)
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("reorder")]
        public async Task<IActionResult> ReorderPageComponents([FromBody] List<ReorderItem> reorderItems)
        {
            foreach (var item in reorderItems)
            {
                var component = await _context.PageComponents.FindAsync(item.Id);
                if (component != null)
                {
                    component.Order = item.Order;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PageComponentExists(int id)
        {
            return _context.PageComponents.Any(e => e.Id == id);
        }

        public class ReorderItem
        {
            public int Id { get; set; }
            public int Order { get; set; }
        }
    }
}