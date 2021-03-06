using Ejercicio_Rober_Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejercicio_Rober_Backend.Controllers
{
    [ApiController]
    // [EnableCors]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private IBlobService _blobService;

        public UploadController(IBlobService blobService) {
            _blobService = blobService;
        }
        
        [HttpPost(""), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadProfilePicture() {
            IFormFile file = Request.Form.Files[0];
            if (file == null) return BadRequest();

            var result = await _blobService.UploadFileBlobAsync(
                "primercontenedor",
                file.OpenReadStream(),
                file.ContentType,
                file.FileName);

            var toReturn = result.AbsoluteUri;

            return Ok(new { path = toReturn });
        }

        //Descargar Fichero 

    }
}
