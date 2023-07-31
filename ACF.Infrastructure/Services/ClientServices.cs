using ACF.Infrastructure.Data;
using ACF.Infrastructure.Interfaces.IServices;
using Core.DTOs;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACF.Infrastructure.Services
{
    public class ClientServices : IClientServices
    {
        #region ATTRIBUTES
        private readonly ACFContext _persistenceContext;
        #endregion

        #region CONSTRUCTOR
        public ClientServices(
            ACFContext persistenceContext)
        {
            _persistenceContext = persistenceContext;
        }
        #endregion

        #region METHODS
        public async Task<Clientes> Registrar(ClienteDTO cliente)
        {
            Clientes savedClient = new Clientes()
            {
                PrimerNombre = cliente.PrimerNombre,
                PrimerApellido = cliente.PrimerApellido,
                Edad = cliente.Edad,
                FechaDeCreación = DateTime.Now
            };

            var query = _persistenceContext.Clientes.Attach(savedClient);

            _persistenceContext.SaveChanges();

            return query.Entity;
        }
        public async Task<Clientes> Actualizar(Clientes cliente)
        {



            var query = _persistenceContext.Clientes.Update(cliente);

            _persistenceContext.SaveChanges();

            return query.Entity;
        }

        public async Task<Clientes> GetClient(int id)
        {
            var getclient = _persistenceContext.Clientes
                            .Where(ro => ro.Identificación == id).FirstOrDefault();

            return getclient;
        }

        public async Task<List<Clientes>> GetClients()
        {
            List<Clientes> query = await _persistenceContext.Clientes.ToListAsync();
            //List<ClientesViewModel> lstProducts = query.Select(x => new ClientesViewModel()
            //{
            //    Nombre = x.Roles.Name,
            //    Apellido = x.Products.Name
            //}).ToList();
            return query;
        }
        #endregion
    }
}
