using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WOLAS.AWS
{
    public class Factory
    {
        public Amazon.S3.IAmazonS3 S3Client()
        {
            return Amazon.AWSClientFactory.CreateAmazonS3Client(CONFIG.Endpoint);
        }

        public Amazon.SQS.IAmazonSQS SQSClient()
        {
            return Amazon.AWSClientFactory.CreateAmazonSQSClient(CONFIG.Endpoint);
        }

        public Amazon.SimpleDB.IAmazonSimpleDB SimpleDBClient()
        {
            return Amazon.AWSClientFactory.CreateAmazonSimpleDBClient(CONFIG.Endpoint);
        }
    }
}
