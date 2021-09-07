using Ejercicio_Rober_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ejercicio_Rober_Backend.Services;

namespace Ejercicio_Rober_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase
    {
        private IBlobService _blobService;

        public DeleteController(IBlobService blobService) {
            _blobService = blobService;
        }

        [HttpDelete]
        [Route("{name}")]
        public void DeleteItemByName(string name) {
            string fileName = name;
            _blobService.DeleteBlobByName(fileName);
        }

    } // Salida de la clase 
}
