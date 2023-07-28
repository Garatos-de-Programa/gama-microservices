namespace Gama.Domain.Interfaces.FileManagement
{
    public interface IFileManager
    {
        Task<FileStream> GetFileAsync(string path);
        Task<string> UploadAsync(Stream fileStream);
    }
}
