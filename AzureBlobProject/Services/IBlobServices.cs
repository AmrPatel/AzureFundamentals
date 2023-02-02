using System.Runtime.Serialization.Formatters;

namespace AzureBlobProject.Services
{
    public interface IBlobServices
    {
        Task<string> GetBlob(string name, string contaainerName);
        Task<List<string>> GetAllBlobs(string contaainerName);
        Task<bool> UploadBlob(string name, IFormFile file,string containerName);
        Task<bool> DeleteBlob(string name,string containerName);
    }
}
