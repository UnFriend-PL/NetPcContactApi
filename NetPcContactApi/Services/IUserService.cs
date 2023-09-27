using NetPcContactApi.Models.User;


namespace NetPcContactApi.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> Register(UserDto userDto);
        Task<ServiceResponse<UserResponse>> Authenticate(UserLoginDto userDto);
        Task<ServiceResponse<UserResponse>> UpdateUser(string jwt, UpdateUserDto userDto);
        Task<ServiceResponse<List<UserResponse>>> GetUsers();
        //Task<ServiceResponse<string>> UpdateUserPasswordAsync(string jwt, UpdateUserPasswordDto userPasswordDto);
    }
}
