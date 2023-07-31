using System.ComponentModel.DataAnnotations;

namespace ACF.WebPage.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese usuario, por favor")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese contraseña por favor")]
        public string Contraseña { get; set; }
    }
}
