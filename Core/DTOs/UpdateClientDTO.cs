using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UpdateClientDTO
    {
        public int Identificación { get; set; }
        public string PrimerNombre { get; set; }
        public string PrimerApellido { get; set; }
        public int Edad { get; set; }
    }
}
