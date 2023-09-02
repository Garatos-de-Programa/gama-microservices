using Microsoft.AspNetCore.Http;

namespace Gama.Domain.ValueTypes
{
    public class FileObject
    {
        public Stream File { get; }

        public string Name { get; set; }

        public FileObject(IFormFile file)
        {
            File = file.OpenReadStream();
            var fileExtension = Path.GetExtension(file.FileName);
            Name = $"{Guid.NewGuid}.{fileExtension}";
        }

        public FileObject(Stream file, S3File s3File)
        {
            File= file;
            Name = s3File.GetFileName();
        }
    }
}
