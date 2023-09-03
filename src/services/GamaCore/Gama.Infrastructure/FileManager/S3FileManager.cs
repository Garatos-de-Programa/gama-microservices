using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Gama.Application.Options;
using Gama.Domain.Interfaces.FileManagement;
using Gama.Domain.ValueTypes;
using Microsoft.Extensions.Options;

namespace Gama.Infrastructure.FileManager
{
    internal class S3FileManager : IFileManager
    {
        private readonly S3Options _s3Options;
        private readonly IAmazonS3 _amazonS3;

        public S3FileManager(
            IAmazonS3 amazonS3,
            IOptions<S3Options> s3Options
            )
        {
            _amazonS3 = amazonS3;
            _s3Options = s3Options.Value;
        }

        public async Task<FileObject> GetFileAsync(string path, CancellationToken cancellationToken)
        {
            var isInvalidUrl = !S3File.IsValidUrl(path, _s3Options.BucketName!);

            if (isInvalidUrl)
                throw new InvalidOperationException("Operação invalida!");

            var fileKey = S3File.GetKey(path);

            var fileObject = await _amazonS3.GetObjectStreamAsync(
                _s3Options.BucketName,
                fileKey, 
                default, 
                cancellationToken: cancellationToken);

            return new FileObject(
                fileObject, 
                new S3File(_s3Options.BucketName!, path)
                );
        }

        public async Task<string> UploadAsync(FileObject fileObject, CancellationToken cancellationToken)
        {
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = fileObject.File,
                Key = fileObject.Name,
                BucketName = _s3Options.BucketName,
                CannedACL = S3CannedACL.NoACL
            };

            using var transferUtility = new TransferUtility(_amazonS3);
            await transferUtility.UploadAsync(uploadRequest, cancellationToken);
            return S3File.ToString(_s3Options.BucketName!, fileObject.Name!);
        }
    }
}
