using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private CalificacionDAO _cdao = new CalificacionDAO();

        #region
        [HttpGet("calificaciones")]
        public List<Calificacion> get(int idMatricula)
        {
            return _cdao.seleccion(idMatricula);
        }
        #endregion
    }
}
