using Gama.Domain.Interfaces.FileManagement;

namespace Gama.Infrastructure.FileManager
{
    internal class LocalFileManager : IFileManager
    {
        public Task<FileStream> GetFileAsync(string path)
        {
            return Task.FromResult(File.OpenRead(path));
        }

        public async Task<string> UploadAsync(Stream fileStream)
        {
            string tempfile = CreateTempfilePath();
            using var stream = File.OpenWrite(tempfile);
            await fileStream.CopyToAsync(stream);

            return tempfile;
        }

        static string CreateTempfilePath()
        {
            var filename = $@"{DateTime.Now.Ticks}.jpg";

            var directoryPath = Path.Combine("files", "uploads");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            return Path.Combine(directoryPath, filename);
        }
    }
}
