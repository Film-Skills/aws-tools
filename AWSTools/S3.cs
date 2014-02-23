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
        /// <returns></returns>
        public static string PreSignURL(string key, string bucket)
        {
            using (Amazon.S3.IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client("", "", Amazon.RegionEndpoint.APSoutheast2))
            {
                Amazon.S3.Model.GetPreSignedUrlRequest request = new Amazon.S3.Model.GetPreSignedUrlRequest();
                request.BucketName = bucket;
                request.Key = key;
                request.Protocol = Amazon.S3.Protocol.HTTPS;
            }
            return string.Empty;
        }
    }
}
