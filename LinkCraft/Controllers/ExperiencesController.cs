using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkCraft.Models;
using LinkCraft.Data;
using Microsoft.AspNetCore.Authorization;

namespace LinkCraft.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly LinkCraftContext _context;

        public ExperiencesController(LinkCraftContext context)
        {
            _context = context;
        }

        // GET: api/Experiences
        [HttpGet]
        public IEnumerable<Experience> GetExperience()
        {
            var owner = User.Identity.Name;
            return _context.Experience.Where(x=> x.UserId == owner).ToList();
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperience([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var experience = await _context.Experience.FindAsync(id);

                if (experience == null || experience.UserId != User.Identity.Name)
                {
                    return NotFound();
                }

                return Ok(experience);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // PUT: api/Experiences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperience([FromRoute] Guid id, [FromBody] Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != experience.Id)
            {
                return BadRequest(new { Error = "Ids do not match" });
            }

            if(experience.UserId != User.Identity.Name)
            {
                return BadRequest(new { Error = "You are not allowed to edit this experience" });
            }

            _context.Entry(experience).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

            return NoContent();
        }

        // POST: api/Experiences
        [HttpPost]
        public async Task<IActionResult> PostExperience([FromBody] PostExperienceViewModel experience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newExperience = new Experience
            {
                Code = experience.Code,
                Url = experience.Url,
                UserId = User.Identity.Name
            };
            try
            {
                _context.Experience.Add(newExperience);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

            return CreatedAtAction("GetExperience", new { id = newExperience.Id }, newExperience);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            if(experience.UserId != User.Identity.Name)
            {
                return BadRequest(new { Error = "You are not allowed to remove this experiece as you are not the owner" } );
            }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync();

            return Ok(experience);
        }

        private bool ExperienceExists(Guid id)
        {
            return _context.Experience.Any(e => e.Id == id);
        }
    }
}