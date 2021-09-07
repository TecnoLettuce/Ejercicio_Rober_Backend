using Ejercicio_Rober_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Ejercicio_Rober_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private IBlobService _blobService;

        public DownloadController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet(""), DisableRequestSizeLimit]
        public async Task<FileStream> DownloadAllBlobs()
        {

            FileStream fileStream = await _blobService.DownloadFileBlob("primercontenedor");
            Response.StatusCode = 200;
            Response.ContentType = "application/zip";
            Response.Body = fileStream;
            return fileStream;
        }

        // TODO Falta el endpoint de descargar por nombre 
    }
}
