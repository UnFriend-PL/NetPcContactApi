namespace NetPcContactApi.Models.User
{
    public class UserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Phone { get; set; } = string.Empty;
        public int ContactCategoryId { get; set; } = 1;
        public int SubContactCategory { get; set; } = 1;
        public DateTime Birthday { get; set; } = new DateTime();
    }
}
