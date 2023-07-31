using ACF.Infrastructure.Interfaces.IRepositories;
using ACF.Infrastructure.Interfaces.IServices;
using Core.DTOs;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        #region ATTRIBUTES
        private readonly IClientServices _iClientsServices;
        #endregion

        #region CONSTRUCTOR
        public ClientRepository(IClientServices ClientServices)
        {
            _iClientsServices = ClientServices;
        }
        #endregion

        #region METHODS
        public async Task<Clientes> Registrar(ClienteDTO cliente)
        {
            return await _iClientsServices.Registrar(cliente);
        }
        public async Task<Clientes> Actualizar(Clientes cliente)
        {
            return await _iClientsServices.Actualizar(cliente);
        }
        public async Task<Clientes> GetClient(int id)
        {
            return await _iClientsServices.GetClient(id);
        }
        public async Task<List<Clientes>> GetClients()
        {
            return await _iClientsServices.GetClients();
        }
        #endregion
    }
}
