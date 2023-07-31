using Core.DTOs;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Interfaces.IServices
{
    public interface IClientServices
    {
        Task<Clientes> Registrar(ClienteDTO cliente);
        Task<List<Clientes>> GetClients();
    }
}