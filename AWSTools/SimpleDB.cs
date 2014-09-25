using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSTools
{
    public class SimpleDB
    {
        /// <summary>
        /// Puts a List of attributes to the specified domain.
        /// Optionally, an item name can be specified but if null
        /// a GUID will be generated.
        /// </summary>
        /// <param name="domain">The domain to write to.</param>
        /// <param name="attributes">The attributes to PUT to the domain.</param>
        /// <param name="itemName">The unique item name for the attributes.</param>
        public static Amazon.SimpleDB.Model.PutAttributesResponse PutAttributes(string domain, List<Amazon.SimpleDB.Model.ReplaceableAttribute> attributes, string itemName = null)
        {
            Amazon.SimpleDB.Model.PutAttributesResponse response = new Amazon.SimpleDB.Model.PutAttributesResponse();
            using (Amazon.SimpleDB.IAmazonSimpleDB client = new Factory().SimpleDBClient())
            {
                Amazon.SimpleDB.Model.PutAttributesRequest request = new Amazon.SimpleDB.Model.PutAttributesRequest()
                {
                    DomainName = domain,
                    ItemName = itemName == null ? Guid.NewGuid().ToString() : itemName,
                    Attributes = attributes
                };
                response = client.PutAttributes(request);
            }
            return response;
        }
    }
}
