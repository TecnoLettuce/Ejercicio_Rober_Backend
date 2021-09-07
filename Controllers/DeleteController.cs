using Ejercicio_Rober_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicio_Rober_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase
    {
        private IBlobService _blobService;
        //private HttpResponse _httpResponse;

        public DeleteController(IBlobService blobService)
        {
            
            _blobService = blobService;
        }

        [HttpDelete]
        [Route("{name}")]
        public IActionResult DeleteItemByName(string name)
        {
            string fileName = name;
            if(_blobService.DeleteBlobByName(fileName))
            {
                return Ok();
            }

            return BadRequest();
        }

    } // Salida de la clase 
}
