using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Web;

namespace Ejercicio_Rober_Backend.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

      //  private HttpResponse httpResponse;

        public BlobService(BlobServiceClient blobServiceClient)
        {
           
            _blobServiceClient = blobServiceClient;
        }

        public async Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri;
        }



        public async Task<FileStream> DownloadFileBlob(string blobContainerName)
        {
            // Directorio donde se va a guardar 
            string downloadPath = Directory.GetCurrentDirectory() + "\\download\\";
            Directory.CreateDirectory(downloadPath);

            // Opciones de transferencia 
            var options = new StorageTransferOptions
            {
                MaximumConcurrency = 8,
                MaximumTransferSize = 50 * 1024 * 1024
            };

            BlobContainerClient containerClient = GetContainerClient(blobContainerName);

            var tasks = new Queue<Task<Response>>();

            foreach (var blobItem in containerClient.GetBlobs())
            {
                // Nombre de salida del fichero 
                string fileDownloadedName = downloadPath + blobItem.Name;

                BlobClient blob = containerClient.GetBlobClient(blobItem.Name);

                tasks.Enqueue(blob.DownloadToAsync(fileDownloadedName, default, options));
            }

            // Ponemos a correr todas las descargas 
            await Task.WhenAll(tasks);

            FileStream fileStream = await CompressFiles("C:\\Proyectos\\Ejercicio_Rober_Backend\\download");
            return fileStream;

        }

        private async Task<FileStream> CompressFiles(string directoryPath)
        {
            ZipFile.CreateFromDirectory(directoryPath, "C:\\Proyectos\\Ejercicio_Rober_Backend\\CompressedFiles.zip");

            return new FileStream(
                "C:\\Proyectos\\Ejercicio_Rober_Backend\\CompressedFiles.zip",
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.ReadWrite);
        }

        /**
         * @return List<FileStream>
         * 
         * Get files from downloads directory
         * */
        public List<FileStream> GetFilesFromServer()
        {
            List<FileStream> ret = new List<FileStream>();
            string directoryPath = "C:\\Proyectos\\Ejercicio_Rober_Backend\\download";

            var downloadedFiles = Directory.EnumerateFiles(directoryPath);
            foreach (var fileName in downloadedFiles)
            {
                ret.Add(new FileStream(fileName, FileMode.Open));
            }

            return ret;
        }


        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }

        void IBlobService.CompressFiles(string directoryPath)
        {
            ZipFile.CreateFromDirectory(directoryPath, "C:\\Proyectos\\Ejercicio_Rober_Backend\\CompressedFiles.zip");
        }

        public bool DeleteBlobByName(string name)
        {
            
            var containerClient = _blobServiceClient.GetBlobContainerClient("primercontenedor");
            BlobClient blobClient = containerClient.GetBlobClient(name);

            if (!blobClient.Exists())
            {
                return false;
            }

            blobClient.Delete();
            
            return true;
        }
    }
}
