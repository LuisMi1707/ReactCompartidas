using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private ProfesorDAO _proDAO = new ProfesorDAO();

        [HttpPost("autenticacion")]

        public string loginProfesor([FromBody] Profesor prof)
        {
            var profl = _proDAO.login(prof.Usuario, prof.Pass);
            if (profl != null)
            {
                return prof.Usuario;
            }
            return "Elemento no encontrado";

        }
    }
}
