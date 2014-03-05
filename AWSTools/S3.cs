using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSTools
{
    public static class S3
    {
        /// <summary>
        /// Takes a key and a bucket and converts the
        /// path into a presigned url for access to restricted
        /// S3 content.
        /// </summary>
        /// <param name="bucket">The bucket that the object is in.</param>
        /// <param name="key">The path to the object in S3.</param>
        /// <param name="expiresIn">The amount of minutes from now to expire the URL from.</param>
        /// <param name="protocol">The protocol of the URL to request. Default is HTTP.</param>
        /// <returns></returns>
        public static string PreSignURL(string bucket, string key, double expiresIn = 5, Amazon.S3.Protocol protocol = Amazon.S3.Protocol.HTTP)
        {
            string url = string.Empty;
            using (Amazon.S3.IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(CONFIG.AccessKey, CONFIG.SecretKey, CONFIG.Endpoint))
            {
                Amazon.S3.Model.GetPreSignedUrlRequest request = new Amazon.S3.Model.GetPreSignedUrlRequest()
                {
                    BucketName = bucket,
                    Key = key,
                    Protocol = protocol,
                    Expires = DateTime.Now.AddMinutes(expiresIn)
                };
                url = client.GetPreSignedURL(request);
            }
            return url;
        }

        /// <summary>
        /// Gets an S3 object as a byte array for use in downloads
        /// or local/DB storage.
        /// </summary>
        /// <param name="bucket">The bucket that the object is in.</param>
        /// <param name="key">The path to the object in S3.</param>
        public static byte[] GetObject(string bucket, string key)
        {
            byte[] data;
            Amazon.S3.Model.GetObjectResponse response = GetObjectResponse(bucket, key);
            using (var ms = new MemoryStream())
            {
                response.ResponseStream.CopyTo(ms);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Gets an S3 object as a GetObjectResponse for
        /// access to other response properties.
        /// </summary>
        /// <param name="bucket">The bucket that the object is in.</param>
        /// <param name="key">The path to the object in S3.</param>
        public static Amazon.S3.Model.GetObjectResponse GetObjectResponse(string bucket, string key)
        {
            Amazon.S3.Model.GetObjectResponse response = new  Amazon.S3.Model.GetObjectResponse();
            using (Amazon.S3.IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(CONFIG.AccessKey, CONFIG.SecretKey, CONFIG.Endpoint)) {
                Amazon.S3.Model.GetObjectRequest request = new Amazon.S3.Model.GetObjectRequest() {
                    BucketName = bucket,
                    Key = key
                };
                response = client.GetObject(request);
            }
            return response;
        }

        /// <summary>
        /// Overload method for getting S3 objects in a bucket.
        /// Gets 1000 objects max.
        /// </summary>
        /// <param name="bucket">The bucket to get objects for.</param>
        public static List<Amazon.S3.Model.S3Object> ListObjects(string bucket)
        {
            Amazon.S3.Model.ListObjectsResponse response = ListObjectsResponse(bucket);
            return response.S3Objects;
        }

        /// <summary>
        /// Overload method for getting S3 objects in a bucket.
        /// Gets 1000 objects max.
        /// </summary>
        /// <param name="bucket">The bucket to get objects for.</param>
        /// <param name="prefix">The prefix to use to search/filter objects.</param>
        public static List<Amazon.S3.Model.S3Object> ListObjects(string bucket, string prefix)
        {
            Amazon.S3.Model.ListObjectsResponse response = ListObjectsResponse(bucket, prefix);
            return response.S3Objects;
        }

        /// <summary>
        /// Overload method for getting S3 objects in a bucket.
        /// Gets 1000 objects max.
        /// </summary>
        /// <param name="bucket">The bucket to get objects for.</param>
        /// <param name="prefix">The prefix to use to search/filter objects.</param>
        /// <param name="delimiter">The delimiter to use to further filter/restrict objects.</param>
        public static List<Amazon.S3.Model.S3Object> ListObjects(string bucket, string prefix, string delimiter)
        {
            Amazon.S3.Model.ListObjectsResponse response = ListObjectsResponse(bucket, prefix, delimiter);
            return response.S3Objects;
        }

        /// <summary>
        /// Overload method for getting S3 objects in a bucket.
        /// Gets 1000 objects max.
        /// </summary>
        /// <param name="bucket">The bucket to get objects for.</param>
        /// <param name="prefix">The prefix to use to search/filter objects.</param>
        /// <param name="delimiter">The delimiter to use to further filter/restrict objects.</param>
        /// <param name="marker">The key to start with to get 1000 objects from.</param>
        public static List<Amazon.S3.Model.S3Object> ListObjects(string bucket, string prefix, string delimiter, string marker)
        {
            Amazon.S3.Model.ListObjectsResponse response = ListObjectsResponse(bucket, prefix, delimiter, marker: marker);
            return response.S3Objects;
        }

        /// <summary>
        /// Lists objects from the specified S3 bucket.
        /// </summary>
        /// <param name="bucket">The name of the bucket to get objects from.</param>
        /// <param name="prefix">The prefix used to filter any results. For example if you had
        /// a folder called img in the root of your bucket, the prefix used would be img/ to 
        /// get all keys in that folder with a delimiter of / to only return keys in that folder, not subfolders.</param>
        /// <param name="delimiter">Used to specify the end of what to return keys for. For example, to
        /// get all keys in folder a in the structure a/b/keys, you would use a/ as the prefix and
        /// / as the delimiter to only get keys inside a.</param>
        /// <param name="maxKeys">The maximum number of keys to return.</param>
        /// <param name="marker">The key to start with when returning keys. Keys are stored in alphabetical order in s3.</param>
        public static Amazon.S3.Model.ListObjectsResponse ListObjectsResponse(string bucket,
                                                                              string prefix = "",
                                                                              string delimiter = null,
                                                                              int maxKeys = 1000,
                                                                              string marker = null)
        {
            Amazon.S3.Model.ListObjectsResponse response = new Amazon.S3.Model.ListObjectsResponse();
            using (Amazon.S3.IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(CONFIG.AccessKey, CONFIG.SecretKey, CONFIG.Endpoint))
            {
                Amazon.S3.Model.ListObjectsRequest request = new Amazon.S3.Model.ListObjectsRequest()
                {
                    BucketName = bucket,
                    Prefix = prefix,
                    MaxKeys = maxKeys
                };

                if (delimiter != null)
                    request.Delimiter = delimiter;

                if (marker != null)
                    request.Marker = marker;

                response = client.ListObjects(request);
            }
            return response;
        }

        public static Amazon.S3.Model.PutObjectResponse PutObjectResponse(string bucket, string key, Stream stream, Amazon.S3.S3StorageClass storageClass = null)
        {
            Amazon.S3.Model.PutObjectResponse response = new Amazon.S3.Model.PutObjectResponse();
            using (Amazon.S3.IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(CONFIG.AccessKey, CONFIG.SecretKey, CONFIG.Endpoint))
            {
                Amazon.S3.Model.PutObjectRequest request = new Amazon.S3.Model.PutObjectRequest()
                {
                    BucketName = bucket,
                    Key = key,
                    StorageClass = (storageClass == null ? Amazon.S3.S3StorageClass.Standard : storageClass),
                    InputStream = stream
                };

                response = client.PutObject(request);
            }
            return response;
        }

        public static Amazon.S3.Model.PutObjectResponse PutObject(string bucket, string key, FileStream stream, Amazon.S3.S3StorageClass storageClass = null)
        {
            return PutObjectResponse(bucket, key, stream, storageClass);
        }

        public static Amazon.S3.Model.PutObjectResponse PutObject(string bucket, string key, byte[] data, Amazon.S3.S3StorageClass storageClass = null)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            return PutObjectResponse(bucket, key, stream, storageClass);
        }
    }
}
