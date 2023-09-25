﻿using Newtonsoft.Json;

namespace NetPcContactApi.Models.User
{
    public class UpdateUserDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("surname")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;
        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

    }
}
