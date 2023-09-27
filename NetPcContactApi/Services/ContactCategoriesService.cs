using Microsoft.EntityFrameworkCore;
using NetPcContactApi.Database;
using NetPcContactApi.Models.Categories;
using NetPcContactApi.Models.User;

namespace NetPcContactApi.Services
{
    public class ContactCategoriesService : IContactCategoriesService
    {
        private readonly DataContext _context;
        public readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactCategoriesService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ServiceResponse<List<ContactCategory>>> GetContactCategories()
        {
            var serviceResponse = new ServiceResponse<List<ContactCategory>>();

            try
            {
                var categories = await _context.ContactCategories
                    .Select(c => new ContactCategory
                    {
                        ContactCategoryId = c.ContactCategoryId,
                        Name = c.Name,
                    })
                    .ToListAsync();

                serviceResponse.Data = categories;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wystąpił błąd podczas pobierania listy kategorii.";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ContactSubCategory>>> GetContactSubCategories(int categoryId)
        {

            var serviceResponse = new ServiceResponse<List<ContactSubCategory>>();

            try
            {
                var categories = await _context.SubContactCategories.Where(c=> c.ContactCategoryId == categoryId)
                    .Select(c => new ContactSubCategory
                    {
                        ContactSubCategoryId = c.ContactCategoryId,
                        Name = c.Name,
                        ContactCategoryId = c.ContactCategoryId
                    })
                    .ToListAsync();

                serviceResponse.Data = categories;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                //serviceResponse.Message = "Wystąpił błąd podczas pobierania listy subkategorii.";
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        //public Task<ServiceResponse<User>> GetSubContactCategoryId(int subCategoryId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
