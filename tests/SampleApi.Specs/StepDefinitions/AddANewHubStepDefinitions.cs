using Newtonsoft.Json;
using SampleApi.Models;
using SampleApi.Specs.Support;

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
            var hub = new Hub
            {
                Name = string.IsNullOrWhiteSpace(name) ? null : name,
                MainContactEmail = string.IsNullOrWhiteSpace(email) ? null : email,
                AlternateEmail = string.IsNullOrWhiteSpace(altemail) ? null : altemail
            };

            var response = ApiTestAccess.CreateHub(hub);

            if (response.IsSuccessStatusCode)
            {
                hub = JsonConvert.DeserializeObject<Hub>(response.Content ?? "{}");
                if (hub != null)
                {
                    ApiTestAccess.DeleteHub(hub.Id);
                }
            }

            _scenarioContext.Add("StatusCode", (int)response.StatusCode);
            _scenarioContext.Add("Hub", hub);
        }

        [Then(@"the API should return (.*)")]
        public void ThenTheAPIShouldReturn(int statusCode)
        {
            Assert.Equal(statusCode, _scenarioContext["StatusCode"]);
        }

        [Given(@"the Client adds new hub with an empty flight list")]
        public void GivenTheClientAddsNewHubWithAnEmptyFlightList()
        {
            var hub = new Hub
            {
                Name = Guid.NewGuid().ToString(),
                MainContactEmail = "testhubwithnoflights@example.com"
            };

            var response = ApiTestAccess.CreateHub(hub);
            response.IsSuccessStatusCode.Should().BeTrue();

            hub = JsonConvert.DeserializeObject<Hub>(response.Content ?? "{}");
            hub.Should().NotBeNull();
            if (hub != null)
            {
                ApiTestAccess.DeleteHub(hub.Id);
            }

            _scenarioContext.Add("Hub", hub);
        }

        [Then(@"the API should create a new hub with no flights")]
        public void ThenTheAPIShouldCreateANewHubWithNoFlights()
        {
            var hub = _scenarioContext.Get<Hub>("Hub");
            hub.Flights.Should().BeNull();
        }

        [Given(@"the Client adds new hub with a flight for tomorrow")]
        public void GivenTheClientAddsNewHubWithAFlightForTomorrow()
        {
            var flightName = Guid.NewGuid().ToString()[..10];
            var flightDate = DateTime.Now.AddDays(1);

            var hub = new Hub
            {
                Name = Guid.NewGuid().ToString(),
                MainContactEmail = "testhubwithoneflight@example.com",
                Flights = new List<Flight> {
                    new Flight
                    {
                        Name = flightName,
                        FlightDate = flightDate
                    }
                }
            };

            var response = ApiTestAccess.CreateHub(hub);
            response.IsSuccessStatusCode.Should().BeTrue();

            hub = JsonConvert.DeserializeObject<Hub>(response.Content ?? "{}");
            hub.Should().NotBeNull();
            if (hub != null)
            {
                ApiTestAccess.DeleteHub(hub.Id);
            }

            _scenarioContext["flightName"] = flightName;
            _scenarioContext["flightDate"] = flightDate;
            _scenarioContext.Add("Hub", hub);
        }

        [Then(@"the API should create a new hub with one flight")]
        public void ThenTheAPIShouldCreateANewHubWithOneFlight()
        {
            var hub = _scenarioContext.Get<Hub>("Hub");
            hub.Flights.Should().NotBeEmpty();
            hub.Flights.Should().HaveCount(1);
        }

        [Then(@"the flight name should match the name provided")]
        public void ThenTheFlightNameShouldMatchTheNameProvided()
        {
            var hub = _scenarioContext.Get<Hub>("Hub");
            var flight = hub.Flights.First();
            flight.Name.Should().Be(_scenarioContext["flightName"].ToString());
        }

        [Then(@"the flight date should match the date provided")]
        public void ThenTheFlightDateShouldMatchTheDateProvided()
        {
            var hub = _scenarioContext.Get<Hub>("Hub");
            var flight = hub.Flights.First();
            flight.FlightDate.Should().Be(_scenarioContext.Get<DateTime>("flightDate"));
        }

        [Given(@"the Client adds new hub with a flight for tomorrow and one for next month")]
        public void GivenTheClientAddsNewHubWithAFlightForTomorrowAndOneForNextMonth()
        {
            var flight1Name = Guid.NewGuid().ToString()[..10];
            var flight1Date = DateTime.Now.AddDays(1);
            var flight2Name = Guid.NewGuid().ToString()[..10];
            var flight2Date = DateTime.Now.AddMonths(1);

            var hub = new Hub
            {
                Name = Guid.NewGuid().ToString(),
                MainContactEmail = "testhubwithtwoflights@example.com",
                Flights = new List<Flight> {
                    new Flight
                    {
                        Name = flight1Name,
                        FlightDate = flight1Date
                    },
                    new Flight
                    {
                        Name = flight2Name,
                        FlightDate = flight2Date
                    }
                }
            };

            var response = ApiTestAccess.CreateHub(hub);
            response.IsSuccessStatusCode.Should().BeTrue();

            hub = JsonConvert.DeserializeObject<Hub>(response.Content ?? "{}");
            hub.Should().NotBeNull();
            if (hub != null)
            {
                ApiTestAccess.DeleteHub(hub.Id);
            }

            _scenarioContext["flight1Name"] = flight1Name;
            _scenarioContext["flight1Date"] = flight1Date;
            _scenarioContext["flight2Name"] = flight2Name;
            _scenarioContext["flight2Date"] = flight2Date;
            _scenarioContext.Add("Hub", hub);
        }

        [Then(@"the API should create a new hub with two flights")]
        public void ThenTheAPIShouldCreateANewHubWithTwoFlights()
        {
            var hub = _scenarioContext.Get<Hub>("Hub");
            hub.Flights.Should().NotBeEmpty();
            hub.Flights.Should().HaveCount(2);
        }

        [Then(@"tomorrow's flight should match the details provided")]
        public void ThenTomorrowsFlightNameShouldMatchTheNameProvided()
        {
            var hub = _scenarioContext.Get<Hub>("Hub");
            var flight = hub.Flights.FirstOrDefault(f => f.Name == _scenarioContext["flight1Name"].ToString());
            flight.Should().NotBeNull();
            flight.FlightDate.Should().Be(_scenarioContext.Get<DateTime>("flight1Date"));
        }

        [Then(@"next month's flight should match the details provided")]
        public void ThenNextMonthsFlightNameShouldMatchTheNameProvided()
        {
            var hub = _scenarioContext.Get<Hub>("Hub");
            var flight = hub.Flights.FirstOrDefault(f => f.Name == _scenarioContext["flight2Name"].ToString());
            flight.Should().NotBeNull();
            flight.FlightDate.Should().Be(_scenarioContext.Get<DateTime>("flight2Date"));
        }

    }
}
