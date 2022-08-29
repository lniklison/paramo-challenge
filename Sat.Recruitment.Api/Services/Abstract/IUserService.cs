using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Abstract
{
    public interface IUserService
    {
        public Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money);
    }
}

