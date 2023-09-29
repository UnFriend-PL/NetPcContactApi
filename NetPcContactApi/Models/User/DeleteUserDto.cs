using Newtonsoft.Json;

namespace NetPcContactApi.Models.User
{
    public class DeleteUserDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
