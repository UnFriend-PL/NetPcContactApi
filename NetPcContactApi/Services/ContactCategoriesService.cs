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

        public async Task<ServiceResponse<List<ContactSubCategory>>> GetContactSubCategories()
        {

            var serviceResponse = new ServiceResponse<List<ContactSubCategory>>();

            try
            {
                var categories = await _context.SubContactCategories
                    .Select(c => new ContactSubCategory
                    {
                        ContactSubCategoryId = c.ContactSubCategoryId,
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

        public async Task<ServiceResponse<ContactSubCategory>> CreateContactSubCategory(ContactSubCategoryDto contactSubCategoryDto)
        {
            var serviceResponse = new ServiceResponse<ContactSubCategory>();

            try
            {
                var category = await _context.SubContactCategories.Where(sb => sb.Name.ToLower() == contactSubCategoryDto.Name.ToLower())
                    .Select(c => new ContactSubCategory
                    {
                        ContactSubCategoryId = c.ContactSubCategoryId,
                        Name = c.Name,
                        ContactCategoryId = c.ContactCategoryId
                    })
                    .FirstOrDefaultAsync();
                if (category != null)
                {
                    serviceResponse.Data = category;
                    return serviceResponse;
                }
                if(category == null)
                {
                    category = new ContactSubCategory { ContactCategoryId = contactSubCategoryDto.ContactCategoryId, Name = contactSubCategoryDto.Name };
                    serviceResponse.Data = category;
                    await _context.SubContactCategories.AddAsync(category);
                    await _context.SaveChangesAsync();
                    return serviceResponse;

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                //serviceResponse.Message = "Wystąpił błąd podczas pobierania listy subkategorii.";
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
