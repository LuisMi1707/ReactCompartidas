using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Models
{
    public class AlumnoProfesor
    {
        public int Id { get; set; }

        public string Dni { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;
        
        public int Edad {  get; set; }

        public string Email { get; set; } = null!;

        public string asignatura { get; set; }
    }
}
