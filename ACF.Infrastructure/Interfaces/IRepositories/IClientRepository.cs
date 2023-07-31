using Core.DTOs;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Interfaces.IRepositories
{
    public interface IClientRepository
    {
        Task<Clientes> Registrar(ClienteDTO cliente);
        Task<List<Clientes>> GetClients();
    }
}