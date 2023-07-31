using ACF.WebPage.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebPage.Extensions;

namespace WebPage.Controllers
{
    public class ClienteController : Controller
    {
        #region ATTRIBUTES
        private readonly ILogger<ClienteController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion
        #region CONSTRUCTOR
        public ClienteController(
            ILogger<ClienteController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        [Authorize(Policy = "AccessUser")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("NombreUsuario")))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [Authorize(Policy = "NuevoCliente")]
        public IActionResult CrearPartial()
        {
            return PartialView();
        }
        [Authorize(Policy = "listadoClientes")]
        public IActionResult LstClientePartial()
        {
            return PartialView();
        }
        [Authorize(Policy = "AccessProductC")]
        public IActionResult ProductoCPartial()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index");

            ResultApi resultLogin = await ServiceExtension.ExcuteAPI<ResultApi>(_httpClientFactory, "API", "api/Clientes/RegistrarCliente", ServiceExtension.POST, model);
            if (resultLogin.Data == null)
            {
                //ViewBag.Error = "El usuario o contraseña es incorrecto";
                ModelState.AddModelError("CustomError", "El usuario o contraseña son incorrectos");
                return View("Index", model);
            }

            if (resultLogin.Data != null)
            {


                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                ModelState.AddModelError("CustomError", "El role del usuario no se encontró");
                return View("Index", model);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LstCliente()
        {
            if (!ModelState.IsValid)
                return View("Index");

            ResultApi resultLogin = await ServiceExtension.ExcuteAPI<ResultApi>(_httpClientFactory, "API", "api/Clientes/ObtenerClientes", ServiceExtension.GET,"");
            if (resultLogin.Data == null)
            {
                //ViewBag.Error = "El usuario o contraseña es incorrecto";
                ModelState.AddModelError("CustomError", "El usuario o contraseña son incorrectos");
                return View("Index");
            }

            if (resultLogin.Data != null)
            {


                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                ModelState.AddModelError("CustomError", "El role del usuario no se encontró");
                return View("Index");
            }
        }

    }
}
