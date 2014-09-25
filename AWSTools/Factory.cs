using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSTools
{
    public class Factory
    {
        public Amazon.S3.IAmazonS3 S3Client()
        {
            if (CONFIG.UseIAM)
            {
                return Amazon.AWSClientFactory.CreateAmazonS3Client(CONFIG.Endpoint);
            } else {
                return Amazon.AWSClientFactory.CreateAmazonS3Client(CONFIG.AccessKey, CONFIG.SecretKey, CONFIG.Endpoint);
            }
        }

        public Amazon.SQS.IAmazonSQS SQSClient()
        {
            if (CONFIG.UseIAM)
            {
                return Amazon.AWSClientFactory.CreateAmazonSQSClient(CONFIG.Endpoint);
            }
            else
            {
                return Amazon.AWSClientFactory.CreateAmazonSQSClient(CONFIG.AccessKey, CONFIG.SecretKey, CONFIG.Endpoint);
            }
        }

        public Amazon.SimpleDB.IAmazonSimpleDB SimpleDBClient()
        {
            if (CONFIG.UseIAM)
            {
                return Amazon.AWSClientFactory.CreateAmazonSimpleDBClient(CONFIG.Endpoint);
            }
            else
            {
                return Amazon.AWSClientFactory.CreateAmazonSimpleDBClient(CONFIG.AccessKey, CONFIG.SecretKey, CONFIG.Endpoint);
            }
        }
    }
}
