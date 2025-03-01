using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        #region
        [HttpGet("prueba")]

        public string Get()
        {
            return "Hola Mundo";
        }
        #endregion
    }
}
