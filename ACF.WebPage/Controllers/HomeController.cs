using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using ACF.WebPage.Models;
using WebPage.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace WebPage.Controllers
{
    public class HomeController : Controller
    {
        #region ATTRIBUTES
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region CONSTRUCTOR
        public HomeController(
            ILogger<HomeController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region METHODS
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index");

            ResultApi resultLogin = await ServiceExtension.ExcuteAPI<ResultApi>(_httpClientFactory, "API", "api/Seguridad/ValidarCredenciales", ServiceExtension.POST, model);
            if (resultLogin.Data == null)
            {
                //ViewBag.Error = "El usuario o contraseña es incorrecto";
                ModelState.AddModelError("CustomError", "El usuario o contraseña son incorrectos");
                return View("Index", model);
            }

            //ResultApi resultUser = await ServiceExtension.ExcuteAPI<ResultApi>(_httpClientFactory, "API", "api/Security/ObtenerRolesUsuario", ServiceExtension.POST, model.NombreUsuario);
            if (resultLogin.Data != null)
            {
                var jwt = resultLogin.Data.ToString();
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                HttpContext.Session.SetString("NombreUsuario", token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
                HttpContext.Session.SetString("TypeUser", token.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value);
                IList<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, model.NombreUsuario),
                    new Claim(ClaimTypes.Name, model.NombreUsuario),
                    new Claim(ClaimTypes.Role, token.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                ModelState.AddModelError("CustomError", "El role del usuario no se encontró");
                return View("Index", model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

    }
}