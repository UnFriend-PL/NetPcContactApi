using Newtonsoft.Json;

namespace NetPcContactApi.Models.User
{
    public class UserDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("password")]
        public required string Password { get; set; } = string.Empty;
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;
        [JsonProperty("contactCategoryId")]
        public int ContactCategoryId { get; set; } = 1;
        [JsonProperty("contactSubCategoryId")]
        public int ContactSubCategoryId { get; set; } = 1;
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; } = new DateTime();

    }
}
