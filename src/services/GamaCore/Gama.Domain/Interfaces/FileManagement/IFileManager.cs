using Gama.Domain.ValueTypes;

namespace Gama.Domain.Interfaces.FileManagement
{
    public interface IFileManager
    {
        Task<FileObject> GetFileAsync(string path, CancellationToken cancellationToken);
        Task<string> UploadAsync(FileObject fileObject, CancellationToken cancellationToken);
    }
}
