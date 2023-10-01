using NetPcContactApi.Database;
using NetPcContactApi.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NetPcContactApi;
using NuGet.Common;

namespace NetPcContactApi.Services
{
    /// <summary>
    /// The UserService class provides functionality for registering, authenticating, and updating users.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Registers a new user into the system.
        /// </summary>
        /// <param name="userDto">The user information to register.</param>
        /// <returns>ServiceResponse containing the new User object if successful.</returns>
        public async Task<ServiceResponse<UserResponse>> Register(UserDto userDto)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);

            if (user != null)
            {
                return CreateResponse<UserResponse>(false, "User already exists.");
            }

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User newUser = CreateUser(userDto, passwordHash, passwordSalt, "User");

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            var userResponse = new UserResponse(newUser, "");
            return CreateResponse(true, "User registered successfully.", userResponse);
        }

        /// <summary>
        /// Authenticates a user and returns a response with user data and a token.
        /// </summary>
        /// <param name="userDto">The user login information.</param>
        /// <returns>ServiceResponse containing UserResponse object if successful.</returns>
        public async Task<ServiceResponse<UserResponse>> Authenticate(UserLoginDto userDto)
        {
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Email == userDto.Email);

            if (user == null)
            {
                return CreateResponse<UserResponse>(false, "User not found.");
            }

            if (!VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return CreateResponse<UserResponse>(false, "Password incorrect.");
            }

            await _context.SaveChangesAsync();

            string token = await CreateUserTokenAsync(user);
            UserResponse userResponse = new UserResponse(user, token);

            return CreateResponse(true, "Authentication successful.", userResponse);
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="token">The token of the user to update.</param>
        /// <param name="userDto">The new user information.</param>
        /// <returns>ServiceResponse containing UserResponse object if successful.</returns>
        public async Task<ServiceResponse<UserResponse>> UpdateUser(string token, UpdateUserDto userDto)
        {
            ServiceResponse<UserResponse> response = new ServiceResponse<UserResponse>();

            try
            {
                int userId = GetUserIdFromToken(token);

                User user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }
                else
                {
                    User userToEdit = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userDto.UserId);
                    if (userToEdit == null)
                    {
                        response.Success = false;
                        response.Message = "User to edit not found.";
                    }
                    else
                    {
                        userToEdit.FirstName = userDto.FirstName;
                        userToEdit.LastName = userDto.LastName;
                        userToEdit.Email = userDto.Email;
                        userToEdit.Phone = userDto.Phone;
                        userToEdit.Birthday = userDto.Birthday;
                        userToEdit.ContactCategoryId = userDto.ContactCategoryId;
                        userToEdit.ContactSubCategoryId = userDto.ContactSubCategoryId;
                        //if (!string.IsNullOrEmpty(userDto.Password))
                        //{
                        //    CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                        //    user.PasswordHash = passwordHash;
                        //    user.PasswordSalt = passwordSalt;
                        //}


                        _context.Users.Update(userToEdit);
                        await _context.SaveChangesAsync();

                        UserResponse userResponse = new UserResponse(user, token);
                        response.Data = userResponse;

                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Creates a ServiceResponse with given parameters.
        /// </summary>
        /// <typeparam name="T">The type of data in the ServiceResponse.</typeparam>
        /// <param name="success">The success status of the response.</param>
        /// <param name="message">The message of the response.</param>
        /// <param name="data">The data of the response.</param>
        /// <returns>A new ServiceResponse object.</returns>
        private ServiceResponse<T> CreateResponse<T>(bool success, string message, T data = default)
        {
            return new ServiceResponse<T>
            {
                Success = success,
                Message = message,
                Data = data,
            };
        }

        /// <summary>
        /// Creates a User object from the provided UserDto and password hash and salt.
        /// </summary>
        /// <param name="userDto">The user information.</param>
        /// <param name="passwordHash">The hashed password.</param>
        /// <param name="passwordSalt">The salt for the password.</param>
        /// <param name="role">The role of the user.</param>
        /// <returns>A new User object.</returns>
        private User CreateUser(UserDto userDto, byte[] passwordHash, byte[] passwordSalt, string role)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Birthday = userDto.Birthday,
                ContactCategoryId = userDto.ContactCategoryId,
                ContactSubCategoryId = userDto.ContactSubCategoryId
            };
        }

        /// <summary>
        /// Extracts the user's ID from the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token containing the user's ID.</param>
        /// <returns>The user's ID extracted from the token.</returns>
        private int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);

            return userId;
        }

        /// <summary>
        /// Generates a password hash and salt for the provided password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordHash">The output password hash.</param>
        /// <param name="passwordSalt">The output password salt.</param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verifies if a provided password matches the stored hash and salt.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="storedHash">The stored password hash.</param>
        /// <param name="storedSalt">The stored password salt.</param>
        /// <returns>True if the password matches the stored hash and salt, false otherwise.</returns>
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Creates a JWT token for a given user with the specified role. 
        /// </summary>
        /// <param name="user">The user for whom the token is to be created.</param>
        /// <param name="role">The role of the user. Default is "user".</param>
        /// <param name="morePermission">If true, checks if the email is allowed for the role and assigns the role if allowed. Default is false.</param>
        /// <returns>A JWT token for the user.</returns>
        private async Task<string> CreateUserTokenAsync(User user, string role = "user", bool morePermission = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token wygasa po 7 dniach
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "user"));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResponse<List<UserResponse>>> GetUsers()
        {
            var serviceResponse = new ServiceResponse<List<UserResponse>>();

            try
            {
                var users = await _context.Users
                    .Select(u => new UserResponse
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        ContactCategoryId = u.ContactCategoryId,
                        ContactSubCategoryId = u.ContactSubCategoryId,
                        Phone = u.Phone,
                        Birthday = u.Birthday,
                    })
                    .ToListAsync();

                serviceResponse.Data = users;
            }
            catch (Exception ex)
            {
                // Obsługa błędów w przypadku wyjątku.
                serviceResponse.Success = false;
                serviceResponse.Message = "Wystąpił błąd podczas pobierania użytkowników.";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteUser(DeleteUserDto deleteUserDto)
        {
            var serviceResponse = new ServiceResponse<string>();
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == deleteUserDto.UserId);
                if (user == null)
                {
                    serviceResponse.Data = "user can't be deleted.";
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Wystąpił błąd podczas usuwania użytkownika.";
                }
                else
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = "user deleted successful.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wystąpił błąd podczas pobierania użytkowników.";
            }

            return serviceResponse;
        }
    }
}
