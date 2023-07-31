using Core.DTOs;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Interfaces.IRepositories
{
    public interface IClientRepository
    {
        Task<Clientes> Registrar(ClienteDTO cliente);
        Task<Clientes> Actualizar(Clientes cliente);
        Task<Clientes> GetClient(int id);
        Task<bool> Eliminar(Clientes cliente);
        Task<List<Clientes>> GetClients();
    }
}