using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSTools
{
    public static class CONFIG
    {
        public static Amazon.RegionEndpoint Endpoint { get; set; }
        public static string AccessKey { get; set; }
        public static string SecretKey { get; set; }
        public static void SetConfig(string access, string secret, Amazon.RegionEndpoint endpoint)
        {
            CONFIG.Endpoint = endpoint;
            CONFIG.AccessKey = access;
            CONFIG.SecretKey = secret;
        }
    }
}
