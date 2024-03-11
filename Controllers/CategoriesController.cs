using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTestProject1.Data;

namespace WebApplicationTestProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDbContext _dbContext =  new ApiDbContext();

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }
    }
}
