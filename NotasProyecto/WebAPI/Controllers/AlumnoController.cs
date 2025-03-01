using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private AlumnoDAO _dao = new AlumnoDAO();

        #region endPointAlumnoProfesor
        [HttpGet("alumnoProfesor")]

        public List<AlumnoProfesor> GetAlumnoProfesors(string usuario) 
        {
            return _dao.alumnoProfesors(usuario);
            
        }
        #endregion
    }
}
