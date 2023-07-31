using Core.Entities;
using Core.Interfaces.CustomOperation;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<IOperationResult<Usuarios>> LoginUser(string username, string password);
        //Task<IOperationResult<ProfileUser>> GetByUserName(string username);
    }
}