using RestSharp;
using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Specs.Support
{
    internal static class ApiTestAccess
    {
        internal static RestResponse CreateHub(Hub hub)
        {
            var client = new RestClient("https://localhost:44361/api/Hubs");
            var request = new RestRequest().AddJsonBody(hub);
            var response = client.ExecutePost(request);
            return response;
        }

        internal static RestResponse DeleteHub(int hubId)
        {
            var client = new RestClient("https://localhost:44361/api/Hubs/{id}");
            var request = new RestRequest().AddUrlSegment("id", hubId);
            var response = client.Delete(request);
            return response;
        }
    }
}
