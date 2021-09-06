using Ejercicio_Rober_Backend.Services;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Ejercicio_Rober_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private IBlobService _blobService;

        public DownloadController(IBlobService blobService) {
            _blobService = blobService;
        }

        [HttpGet(""), DisableRequestSizeLimit]
        public async Task<FileStream> DownloadAllBlobs(){
            
            FileStream fileStream = await _blobService.DownloadFileBlob("primercontenedor");
            Response.StatusCode = 200;
            Response.ContentType = "application/json";
            Response.Body = fileStream;
            return fileStream;
        }

    }
}
