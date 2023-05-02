using tadoba_api.Models;

namespace tadoba_api.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// this method will perlom login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<Response<UserModel>> Login(LoginModel loginModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<Response<UserModel>> Register(UserModel userModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<Response<UserModel>> UpdateUser(UserModel userModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAddressModel"></param>
        /// <returns></returns>
        Task<Response<UserAddressModel>> AddAddress(UserAddressModel userAddressModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAddressModel"></param>
        /// <returns></returns>
        Task<Response<UserAddressModel>> UpdateAddress(UserAddressModel userAddressModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response<List<UserAddressModel>>> GetAddresses(long userId);
    }
}
