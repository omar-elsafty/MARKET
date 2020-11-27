using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MARKET.Services
{
    public interface IUserServices
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginModel model);
    }
}
