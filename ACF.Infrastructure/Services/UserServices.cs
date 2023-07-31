using ACF.Infrastructure.Data;
using ACF.Infrastructure.Interfaces.IServices;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserServices : IUserServices
    {
        #region ATTRIBUTES
        private readonly ACFContext _persistenceContext;
        #endregion

        #region CONSTRUCTOR
        public UserServices(ACFContext persistenceContext)
        {
            _persistenceContext = persistenceContext;
        }
        #endregion

        #region METHODS
        public async Task<Usuarios> LoginUser(string username, string password)
        {
            Usuarios user = await _persistenceContext.Usuarios
                .Where(sig => sig.NombreUsuario == username && sig.Contraseña == password)
                .FirstOrDefaultAsync();
            return user;
        }

     
        #endregion
    }
}
