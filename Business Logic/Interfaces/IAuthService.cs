using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Interfaces
{
    public interface IAuthService
    { 
        Task<StoreAccount> AuthenticateAsync(string email, string password);

    }
}
