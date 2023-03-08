using System;
using TechTalk.SpecFlow;

namespace SampleApi.Specs.StepDefinitions
{
    [Binding]
    public class AddANewHubStepDefinitions
    {
        [Given(@"the Client adds new hub \(<Name>,<MainContactEmail>,<AlternateEmail>\)")]
        public void GivenTheClientAddsNewHubNameMainContactEmailAlternateEmail()
        {
            throw new PendingStepException();
        }

        [Given(@"Hub model is correct")]
        public void GivenHubModelIsCorrect()
        {
            throw new PendingStepException();
        }

        [Then(@"the API should return <StatusCode>")]
        public void ThenTheAPIShouldReturnStatusCode()
        {
            throw new PendingStepException();
        }
    }
}
