using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOLAS.AWS
{
    public static class CONFIG
    {
        internal static Amazon.RegionEndpoint Endpoint { get; set; }
        public static void SetConfig(Amazon.RegionEndpoint endpoint)
        {
            CONFIG.Endpoint = endpoint;
        }
    }
}
