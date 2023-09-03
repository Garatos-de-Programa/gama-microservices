using Gama.Domain.Seedworks;
using Microsoft.AspNetCore.Http;

namespace Gama.Domain.ValueTypes
{
    public class FileObject
    {
        public Stream File { get; }

        public string? Name { get; set; }

        public FileObject(IFormFile file)
        {
            var isNotAcceptedLenth = !IsAcceptedLenth(file);
            var isNotAcceptedExtension = !IsAcceptedExtension(file);

            if (isNotAcceptedLenth)
                throw new ArgumentException("Arquivo muito grande. Você deve informar um até 5 mb.");

            if (isNotAcceptedExtension)
                throw new ArgumentException("Extensão não suportada. Apenas PNG e JPG são aceitos.");

            File = file.OpenReadStream();
            SetName(file);
        }

        public FileObject(Stream file, S3File s3File)
        {
            File= file;
            Name = s3File.GetFileName();
        }

        internal static bool IsAcceptedLenth(IFormFile file)
        {
            var fiveMegas = Sizes.OneMegabyte * 5;
            return file.Length <= fiveMegas;
        }

        internal static bool IsAcceptedExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);

            if (extension.Equals(FilesExtensions.JPG) || extension.Equals(FilesExtensions.PNG))
            {
                return true;
            }

            return false;
        }

        protected void SetName(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            Name = $"{Guid.NewGuid()}{fileExtension}";
        }
    }
}
