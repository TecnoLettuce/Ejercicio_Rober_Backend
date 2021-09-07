using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Ejercicio_Rober_Backend.Services
{
    public interface IBlobService
    {
        Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName);

        Task<FileStream> DownloadFileBlob(string blobContainerName);

        List<FileStream> GetFilesFromServer();

        void CompressFiles(string directoryPath);

        void DeleteBlobByName(string name);
    }
}