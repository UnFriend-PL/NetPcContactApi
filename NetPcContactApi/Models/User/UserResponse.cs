using NetPcContactApi.Models.Categories;
using Newtonsoft.Json;

namespace NetPcContactApi.Models.User
{
    public class UserResponse
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("fistName")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;
        [JsonProperty("contactCategoryId")]
        public int ContactCategoryId { get; set; }
        [JsonProperty("contactSubCategoryId")]
        public int ContactSubCategoryId { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
        public UserResponse()
        {

        }
        public UserResponse(User user, string token)
        {
            UserId = user.UserId; 
            FirstName = user.FirstName; 
            LastName = user.LastName; 
            Email = user.Email; 
            Phone = user.Phone;
            Token = token;
            Birthday = user.Birthday;
            ContactCategoryId = user.ContactCategoryId;
            ContactSubCategoryId = user.ContactSubCategoryId;
        }
    }

}
