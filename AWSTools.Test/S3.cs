using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AWSTools.Test
{
    [TestClass]
    public class S3
    {
        [TestMethod]
        public void PreSignURL()
        {
            AWSTools.CONFIG.SetConfig("AKIAIQF2ZHD4CHMMASHQ", "S8Mjn0vlTv87UmmmOMqRoWK1L1g41wwvXJAm8ij5", Amazon.RegionEndpoint.APSoutheast2);
            string url = AWSTools.S3.PreSignURL("aws-tools-testing", "forrest.jpg", 10);
            Assert.IsTrue(url.Contains("forrest.jpg"));
        }

        [TestMethod]
        public void GetObject()
        {
            AWSTools.CONFIG.SetConfig("AKIAIQF2ZHD4CHMMASHQ", "S8Mjn0vlTv87UmmmOMqRoWK1L1g41wwvXJAm8ij5", Amazon.RegionEndpoint.APSoutheast2);
            byte[] obj = AWSTools.S3.GetObject("aws-tools-testing", "forrest.jpg");
            Assert.IsTrue(obj.Length > 0);
        }

        [TestMethod]
        public void GetObjectResponse()
        {
            AWSTools.CONFIG.SetConfig("AKIAIQF2ZHD4CHMMASHQ", "S8Mjn0vlTv87UmmmOMqRoWK1L1g41wwvXJAm8ij5", Amazon.RegionEndpoint.APSoutheast2);
            Amazon.S3.Model.GetObjectResponse response = AWSTools.S3.GetObjectResponse("aws-tools-testing", "forrest.jpg");
            Assert.AreEqual("image/jpeg", response.Headers.ContentType);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.HttpStatusCode);
        }
        [TestMethod]
        public void ListObjectsResponse()
        {
            AWSTools.CONFIG.SetConfig("AKIAIQF2ZHD4CHMMASHQ", "S8Mjn0vlTv87UmmmOMqRoWK1L1g41wwvXJAm8ij5", Amazon.RegionEndpoint.APSoutheast2);
            Amazon.S3.Model.ListObjectsResponse response = AWSTools.S3.ListObjectsResponse("aws-tools-testing");
            Assert.AreEqual(1, response.CommonPrefixes.Count);
        }
    }
}
