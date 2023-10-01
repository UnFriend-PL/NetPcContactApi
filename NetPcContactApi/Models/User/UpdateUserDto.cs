using Newtonsoft.Json;

namespace NetPcContactApi.Models.User
{
    public class UpdateUserDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; } = new DateTime();
        [JsonProperty("contactCategoryId")]
        public int ContactCategoryId { get; set; } = 1;
        [JsonProperty("contactSubCategoryId")]
        public int ContactSubCategoryId { get; set; } = 1;

    }
}
