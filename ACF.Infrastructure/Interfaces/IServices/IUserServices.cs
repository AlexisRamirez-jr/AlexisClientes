using Core.Entities;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<Usuarios> LoginUser(string username, string password);
        //Task<ProfileUser> GetByUserName(string username);
    }
}