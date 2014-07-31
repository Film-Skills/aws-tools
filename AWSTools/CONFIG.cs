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
        public static bool UseIAM { get; set; }
        public static void SetConfig(string access, string secret, Amazon.RegionEndpoint endpoint, bool useIAM = false)
        {
            CONFIG.Endpoint = endpoint;
            CONFIG.AccessKey = access;
            CONFIG.SecretKey = secret;
            CONFIG.UseIAM = useIAM;
        }
        public static void SetConfig(Amazon.RegionEndpoint endpoint, bool useIAM)
        {
            CONFIG.Endpoint = endpoint;
            CONFIG.UseIAM = useIAM;
        }
    }
}
