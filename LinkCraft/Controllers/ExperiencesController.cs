using System;
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

        [AllowAnonymous]
        [HttpGet("getall")]
        public ResultListModel GetAll()
        {
            try
            {
                return new ResultListModel(_context.Experience.Select(x => new PublicExperience { Code = x.Code, Url = x.Url }).ToList());
            }
            catch (Exception)
            {
                return new ResultListModel("Database error");
            }
        }

        [HttpGet]
        public ResultListModel GetExperience()
        {
            try
            {
                var owner = User.Identity.Name;
                return new ResultListModel(_context.Experience.Where(x => x.UserId == owner).ToList());
            }
            catch (Exception)
            {
                return new ResultListModel("Database error");
            }
            
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ResultModel> GetExperience([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new ResultModel("Invalid Id");
            }

            try
            {
                var experience = await _context.Experience.FindAsync(id);

                if (experience == null || experience.UserId != User.Identity.Name)
                {
                    return new ResultModel("Experience not found");
                }

                return new ResultModel(experience);
            }
            catch (Exception)
            {
                return new ResultModel("Database error");
            }
        }

        // PUT: api/Experiences/5
        [HttpPut("{id}")]
        public async Task<ResultBaseModel<string>> PutExperience([FromRoute] Guid id, [FromBody] Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return new ResultBaseModel<string>("Experience not found");
            }

            if (id != experience.Id)
            {
                return new ResultBaseModel<string>("Ids do not match");
            }

            if(experience.UserId != User.Identity.Name)
            {
                return new ResultBaseModel<string>("You are not allowed to update thi experience");
            }

            _context.Entry(experience).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return new ResultBaseModel<string>("Experience with this code already exists");
            }

            return new ResultBaseModel<string>("Experience updated", true);
        }

        // POST: api/Experiences
        [HttpPost]
        public async Task<ResultModel> PostExperience([FromBody] PostExperienceViewModel experience)
        {
            if (!ModelState.IsValid)
            {
                return new ResultModel("Experience not found");
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
            catch (Exception)
            {
                return new ResultModel("Experience with this code already exists");
            }

            return new ResultModel(experience);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<ResultModel> DeleteExperience([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new ResultModel("Experience not found");
            }

            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
            {
                return new ResultModel("Experience not found");
            }

            if(experience.UserId != User.Identity.Name)
            {
                return new ResultModel("You are not allowed to remove this experience");
            }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync();

            return new ResultModel(experience);
        }

        private bool ExperienceExists(Guid id)
        {
            return _context.Experience.Any(e => e.Id == id);
        }
    }
}