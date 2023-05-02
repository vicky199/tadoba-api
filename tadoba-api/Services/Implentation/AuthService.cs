using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tadoba_api.Entity;
using tadoba_api.Models;
using tadoba_api.Repository;
using tadoba_api.Uow;

namespace tadoba_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<UserAddress> _userAddressRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthService(IGenericRepository<User> userRepo, IGenericRepository<UserAddress> userAddressRepo, IMapper mapper,
                          IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _userAddressRepo = userAddressRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<Response<UserAddressModel>> AddAddress(UserAddressModel userAddressModel)
        {
            UserAddress address = new UserAddress();
            address = _mapper.Map<UserAddress>(userAddressModel);
            address = await _userAddressRepo.AddAsync(address);
            await _unitOfWork.SaveAsync();
            userAddressModel.Id = address.Id;
            return await Response<UserAddressModel>.GenerateResponse(true, userAddressModel, "Address Added Successfully.");
        }

        public async Task<Response<List<UserAddressModel>>> GetAddresses(long userId)
        {
            List<UserAddress> address = new List<UserAddress>();
            address = (await _userAddressRepo.ListAllAsync(x => x.UserId == userId)).ToList();
            List<UserAddressModel> response = _mapper.Map<List<UserAddressModel>>(address);
            return await Response<List<UserAddressModel>>.GenerateResponse(true, response);
        }

        public async Task<Response<UserModel>> Login(LoginModel loginModel)
        {
            UserModel result = new UserModel();
            User user = new User();
            user = await _userRepo.GetByFilterAsync(x => x.Username == loginModel.UserName && x.Password == x.Password && x.IsActive);
            if (user != null && user.Id > 0)
            {
                result = _mapper.Map<UserModel>(user);
                result.AccessToken = await _generateAccessToken(result);
                return await Response<UserModel>.GenerateResponse(true, result, "Login Successfully.");
            }
            else
            {
                return await Response<UserModel>.GenerateResponse(false, result, "Invalid Login.");
            }
        }

        public async Task<Response<UserModel>> Register(UserModel userModel)
        {
            User user = _mapper.Map<User>(userModel);
            await _userRepo.AddAsync(user);
            await _unitOfWork.SaveAsync();
            userModel.Id = user.Id;
            return await Response<UserModel>.GenerateResponse(true, userModel, "You are successfully Sign up ,Please login.");
        }

        public async Task<Response<UserAddressModel>> UpdateAddress(UserAddressModel userAddressModel)
        {
            UserAddress address = new UserAddress();
            address = _mapper.Map<UserAddress>(userAddressModel);
            await _userAddressRepo.UpdateAsync(address);
            await _unitOfWork.SaveAsync();
            userAddressModel.Id = address.Id;
            return await Response<UserAddressModel>.GenerateResponse(true, userAddressModel, "Address updated Successfully.");
        }

        public async Task<Response<UserModel>> UpdateUser(UserModel userModel)
        {
            User user = new User();
            user = _mapper.Map<User>(userModel);
            await _userRepo.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            userModel.Id = user.Id;
            return await Response<UserModel>.GenerateResponse(true, userModel, "Details are updated successfully.");
        }
        private Task<string> _generateAccessToken(UserModel userModel)
        {
            return Task.Run(() =>
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId", userModel.Id.ToString()),
                    new Claim("UserData", JsonConvert.SerializeObject(userModel)),
                    new Claim("Name",userModel.Name)
                    }),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpireTime"])),
                    SigningCredentials = credentials
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            });
        }
    }
}
