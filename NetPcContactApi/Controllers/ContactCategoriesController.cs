using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPcContactApi.Models.Categories;
using NetPcContactApi.Models.User;
using NetPcContactApi.Services;

namespace NetPcContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactCategoriesController : ControllerBase
    {

        public readonly IConfiguration _configuration;
        public readonly IContactCategoriesService _contactCategories;
        public ContactCategoriesController(IConfiguration configuration, IContactCategoriesService contactCategories)
        {
            _configuration = configuration;
            _contactCategories = contactCategories;
        }

        /// <summary>
        /// Get categories collection
        /// </summary>
        /// <returns>Colletion of contact categories</returns>
        [HttpGet("ContactCategories")]
        public async Task<ActionResult<ServiceResponse<User>>> GetContactCategories()
        {
            var response = await _contactCategories.GetContactCategories();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Get users collection
        /// </summary>
        /// <returns>Colletion of contact subcategories</returns>
        [HttpGet("ContactSubCategories")]
        public async Task<ActionResult<ServiceResponse<ContactSubCategory>>> GetContactSubCategories(int contactCategoryId)
        {
            var response = await _contactCategories.GetContactSubCategories(contactCategoryId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
