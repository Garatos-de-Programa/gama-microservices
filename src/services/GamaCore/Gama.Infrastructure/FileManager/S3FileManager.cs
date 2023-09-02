using Amazon.S3;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public S3FileManager(
            IAmazonS3 amazonS3,
            IHttpClientFactory httpClientFactory,
            IOptions<S3Options> s3Options
            )
        {
            _amazonS3 = amazonS3;
            _httpClientFactory = httpClientFactory;
            _s3Options = s3Options.Value;
        }

        public async Task<FileObject> GetFileAsync(string path, CancellationToken cancellationToken)
        {
            var isInvalidUrl = !S3File.IsValidUrl(path, _s3Options.BucketName!);

            if (isInvalidUrl)
                throw new InvalidOperationException("Operação invalida!");

            using var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(path, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            response.EnsureSuccessStatusCode();

            return new FileObject(
                response.Content.ReadAsStream(cancellationToken), 
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
            return S3File.ToString(_s3Options.BucketName!, fileObject.Name);
        }
    }
}
