using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
namespace WOLAS.AWS.Test
{
    [TestClass]
    public class S3
    {
        [TestMethod]
        public void PreSignURL()
        {
            WOLAS.AWS.CONFIG.SetConfig(Amazon.RegionEndpoint.APSoutheast2);
            string url = WOLAS.AWS.S3.PreSignURL("aws-tools-testing", "forrest.jpg", 10);
            Assert.IsTrue(url.Contains("forrest.jpg"));
        }

        [TestMethod]
        public void GetObject()
        {
            WOLAS.AWS.CONFIG.SetConfig(Amazon.RegionEndpoint.APSoutheast2);
            byte[] obj = WOLAS.AWS.S3.GetObject("aws-tools-testing", "forrest.jpg");
            Assert.IsTrue(obj.Length > 0);
        }

        [TestMethod]
        public void GetObjectResponse()
        {
            WOLAS.AWS.CONFIG.SetConfig(Amazon.RegionEndpoint.APSoutheast2);
            Amazon.S3.Model.GetObjectResponse response = WOLAS.AWS.S3.GetObjectResponse("aws-tools-testing", "forrest.jpg");
            Assert.AreEqual("image/jpeg", response.Headers.ContentType);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.HttpStatusCode);
        }

        [TestMethod]
        public void GetObjectMetadataResponse()
        {
            WOLAS.AWS.CONFIG.SetConfig(Amazon.RegionEndpoint.APSoutheast2);
            Amazon.S3.Model.GetObjectMetadataResponse response = WOLAS.AWS.S3.GetObjectMetadataResponse("aws-tools-testing", "forrest.jpg");
            Assert.IsTrue(response.LastModified != null);
        }

        [TestMethod]
        public void ListObjectsResponse()
        {
            WOLAS.AWS.CONFIG.SetConfig(Amazon.RegionEndpoint.APSoutheast2);
            Amazon.S3.Model.ListObjectsResponse response = WOLAS.AWS.S3.ListObjectsResponse("aws-tools-testing", "a/", "/");
            Assert.AreEqual(1, response.CommonPrefixes.Count);
        }
        [TestMethod]
        public void PutObjectResponseFileStream()
        {
            WOLAS.AWS.CONFIG.SetConfig(Amazon.RegionEndpoint.APSoutheast2);
            Amazon.S3.Model.PutObjectResponse response = WOLAS.AWS.S3.PutObject("aws-tools-testing", "1.jpeg", new FileStream(@"C:\Users\Martin\Downloads\1.jpg", FileMode.Open));
            Assert.AreEqual(200, response.HttpStatusCode);
        }
        [TestMethod]
        public void PutObjectResponseByte()
        {
            WOLAS.AWS.CONFIG.SetConfig(Amazon.RegionEndpoint.APSoutheast2);
            Amazon.S3.Model.PutObjectResponse response = WOLAS.AWS.S3.PutObject("aws-tools-testing", "1.jpeg", File.ReadAllBytes(@"C:\Users\Martin\Downloads\1.jpg"));
            Assert.AreEqual(200, response.HttpStatusCode);
        }
    }
}
