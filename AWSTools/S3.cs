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
    }
}
