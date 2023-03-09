using Namotion.Reflection;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow.Assist.ValueRetrievers;
using TechTalk.SpecFlow.Assist;

namespace SampleApi.Specs.StepDefinitions
{
    [Binding]
    public class AddANewHubStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public AddANewHubStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the Client adds new hub \((.*),(.*),(.*)\)")]
        public void GivenTheClientAddsNewHubTestHubHubExample_ComAlthubExample_Com(string name, string email, string altemail)
        {
            var hub = new Models.Hub
            {
                Name = string.IsNullOrWhiteSpace(name) ? null : name,
                MainContactEmail = string.IsNullOrWhiteSpace(email) ? null : email,
                AlternateEmail = string.IsNullOrWhiteSpace(altemail) ? null : altemail
            };

            var client = new RestClient("https://localhost:44361/api/Hubs");
            var request = new RestRequest().AddJsonBody(hub);
            var response = client.ExecutePost(request);

            if (response.IsSuccessStatusCode)
            {
                hub = JsonConvert.DeserializeObject<Models.Hub>(response.Content ?? "{}");
                client = new RestClient("https://localhost:44361/api/Hubs/{id}");
                request = new RestRequest().AddUrlSegment("id", hub.Id);
                client.Delete(request);
            }

            _scenarioContext.Add("StatusCode", (int)response.StatusCode);
            _scenarioContext.Add("Hub", hub);


        }

        [Then(@"the API should return (.*)")]
        public void ThenTheAPIShouldReturn(int statusCode)
        {
            Assert.Equal(statusCode, _scenarioContext["StatusCode"]);
        }
    }
}
