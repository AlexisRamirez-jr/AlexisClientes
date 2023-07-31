using ACF.Infrastructure.Interfaces.IRepositories;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiProductos.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        #region ATTRIBUTES
        private readonly IClientRepository _iProductRepository;
        #endregion

        #region CONSTRUCTOR
        public ClientesController(IClientRepository productRepository)
        {
            _iProductRepository = productRepository;
        }
        #endregion


        #region METHODS

        [HttpPost("RegistrarCliente")]
        public async Task<IActionResult> RegistrarCliente(ClienteDTO cliente)
        {
            Clientes result = await _iProductRepository.Registrar(cliente);
            return Ok(new
            {
                Code = 1,
                Data = result
            });
        }

        [HttpPatch("ActualizarCliente")]
        public async Task<IActionResult> ActualizarCliente(UpdateClientDTO cliente)
        {

            Clientes getclient = await _iProductRepository.GetClient(cliente.Identificación);
            if (getclient == null)
            {
                return Ok(new { Code = StatusCodes.Status404NotFound, Message = "No se encontro el cliente" });
            }
            getclient.PrimerNombre = cliente.PrimerNombre;
            getclient.PrimerApellido = cliente.PrimerApellido;
            getclient.Edad = cliente.Edad;

            Clientes result = await _iProductRepository.Actualizar(getclient);

            if (result == null)
            {
                return Ok(new { Code = StatusCodes.Status404NotFound, Message = "No se encontro el cliente" });
            }
            return Ok(new
            {
                Code = 1,
                Data = result
            });
        }

        [HttpGet("ObtenerClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            List<Clientes> result = await _iProductRepository.GetClients();
            return Ok(new
            {
                Code = 1,
                Data = result
            });
        }
        #endregion
    }
}
