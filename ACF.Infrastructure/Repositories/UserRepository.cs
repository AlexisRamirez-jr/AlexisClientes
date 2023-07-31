using ACF.Infrastructure.Interfaces.IRepositories;
using ACF.Infrastructure.Interfaces.IServices;
using Core.CustomClass;
using Core.Entities;
using Core.Interfaces.CustomOperation;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region ATTRIBUTES
        private readonly IUserServices _iUserServices;
        #endregion

        #region CONSTRUCTOR
        public UserRepository(IUserServices userServices)
        {
            _iUserServices = userServices;
        }
        #endregion

        #region METHODS
        public async Task<IOperationResult<Usuarios>> LoginUser(string username, string password)
        {
            Usuarios user = await _iUserServices.LoginUser(username, password);
            if (user == null)
                return BasicOperationResult<Usuarios>.Fail("El usuario no fue encontrado");
            return BasicOperationResult<Usuarios>.Ok(user);
        }


        #endregion
    }
}
