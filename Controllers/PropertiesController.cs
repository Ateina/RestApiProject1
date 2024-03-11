using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;
using WebApplicationTestProject1.Data;
using WebApplicationTestProject1.Models;

namespace WebApplicationTestProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();

        [HttpGet("GetPropertiesByCategoryId")]
        [Authorize]
        public IActionResult GetPropertiesByCategoryId(int categoryId)
        {
            var propertiesResult = _dbContext.Properties.Where(c => c.CategoryId == categoryId);
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpGet("GetTrendingProperties")]
        [Authorize]
        public IActionResult GetTrendingProperties()
        {
            var propertiesResult = _dbContext.Properties.Where(c => c.IsTrending);
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpGet("GetPropertiesByAddress")]
        [Authorize]
        public IActionResult GetSearchProperties(string address)
        {
            var propertiesResult = _dbContext.Properties.Where(c => c.Address.Contains(address));
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpGet("GetPropertyDetail")]
        [Authorize]
        public IActionResult GetPropertyDetail(int id)
        {
            var property = _dbContext.Properties.FirstOrDefault(prop => prop.Id == id);
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Property property)
        {
            if (property == null) { return NoContent(); }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.First(u => u.Email == userEmail);
                if (user == null)
                {
                    return NotFound();
                }
                property.IsTrending = false;
                property.UserId = user.Id;
                _dbContext.Properties.Add(property);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Property property)
        {
            if (property == null) { return NoContent(); }
            var propertyExists = _dbContext.Properties.FirstOrDefault(property => property.Id == id);
            if (propertyExists == null) { return NotFound(); }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.First(u => u.Email == userEmail);
                if (user == null)
                {
                    return NotFound();
                }
                if(propertyExists.UserId == user.Id)
                {
                    propertyExists.Name = property.Name;
                    propertyExists.Detail = property.Detail;
                    propertyExists.ImageUrl = property.ImageUrl;
                    propertyExists.Price = property.Price;
                    propertyExists.Address = property.Address;
                    _dbContext.SaveChanges();
                    return Ok("Record updated");
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var property = _dbContext.Properties.FirstOrDefault(property => property.Id == id);
            if (property == null) { return NoContent(); }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.First(u => u.Email == userEmail);
                if (user == null)
                {
                    return NotFound();
                }
                if (property.UserId == user.Id)
                {
                    _dbContext.Properties.Remove(property);
                    _dbContext.SaveChanges();
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
        }
    }
}
