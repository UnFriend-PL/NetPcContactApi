using NetPcContactApi.Models.Categories;
using NetPcContactApi.Models.User;

namespace NetPcContactApi.Services
{
    public interface IContactCategoriesService
    {
        Task<ServiceResponse<List<ContactCategory>>> GetContactCategories();
        Task<ServiceResponse<List<ContactSubCategory>>> GetContactSubCategories();
        Task<ServiceResponse<ContactSubCategory>> CreateContactSubCategory(ContactSubCategoryDto contactSubCategoryDto);

    }
}
