namespace Gama.Domain.ValueTypes
{
    public class S3File
    {
        protected const int KeyLength = 40;
        protected const int UrlLength = 84;

        public string FilePath { get; }

        public string BucketName { get; }

        public S3File(string bucketName, string filePath)
        {
            FilePath = filePath;
            BucketName = bucketName;
        }

        public string GetFileName()
        {
            var name = Path.GetFileName(FilePath);
            return name;
        }

        public override string ToString()
        {
            var protocol = FilePath[5..];
            if (protocol == "https")
                return FilePath;

            return $"https://{BucketName}.s3.amazonaws.com/{FilePath}";
        }

        public static string ToString(string bucketName, string filePath)
        {
            var protocol = filePath[..5];
            if (protocol == "https")
                return filePath;

            return $"https://{bucketName}.s3.amazonaws.com/{filePath}";
        }

        public static bool IsValidUrl(string path, string bucketName = "gama.bucket.com.br")
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (path.Length < UrlLength)
                return false;

            var amazonUrlStart = bucketName.Length + 8;
            var amazonUrlEnd = amazonUrlStart + 17;
            var bucket = path[8..amazonUrlStart];
            var amazon = path[amazonUrlStart..amazonUrlEnd];
            if (bucket.Equals(bucketName) && amazon.Equals(".s3.amazonaws.com"))
            {
                return true;
            }

            return false;
        }

        public static string GetKey(string path)
        {
            var keyStart = path.Length - KeyLength;
            var key = path[keyStart..];
            return key;
        }
    }
}
