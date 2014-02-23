using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSTools
{
    public class S3
    {
        /// <summary>
        /// Takes a key and a bucket and converts the
        /// path into a presigned url for access to restricted
        /// S3 content.
        /// </summary>
        /// <param name="key">The path to the file in S3.</param>
        /// <param name="bucket">The bucket that the file is in.</param>
        /// <param name="expiresIn">The expiry time from now in minutes.</param>
        /// <returns></returns>
        public static string PreSignURL(string key, string bucket, double expiresIn = 5)
        {
            string url = string.Empty;
            using (Amazon.S3.IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client("", "", Amazon.RegionEndpoint.APSoutheast2))
            {
                Amazon.S3.Model.GetPreSignedUrlRequest request = new Amazon.S3.Model.GetPreSignedUrlRequest();
                request.BucketName = bucket;
                request.Key = key;
                request.Protocol = Amazon.S3.Protocol.HTTPS;
                request.Expires = DateTime.Now.AddMinutes(expiresIn);
                url = client.GetPreSignedURL(request);
            }
            return url;
        }
    }
}
