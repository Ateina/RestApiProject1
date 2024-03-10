using Microsoft.AspNetCore.Mvc;
using WebApplicationTestProject1.Data;
using WebApplicationTestProject1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationTestProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();

        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound("Record with id " + id + " not found.");
            }
            return Ok(category);
        }
        // api/<CategoriesController>/GetSortCategories
        [HttpGet("[action]")]
        public IActionResult GetSortCategories(int id)
        {
            var categories = _dbContext.Categories.OrderByDescending(x => x.Name);
            return Ok(categories);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
        {
            var category = _dbContext.Categories.Find(id);
            if(category == null)
            {
                return NotFound("Record with id " + id + " not found.");
            }
            category.Name = value.Name;
            category.ImageUrl = value.ImageUrl;
            _dbContext.SaveChanges();
            return Ok("Record updated successfully!");
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Record with id " + id + " not found.");
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return Ok("Record deleted successfully!");
        }
    }
}
