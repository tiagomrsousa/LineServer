using LineServer.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace LineServerTests.Acceptance_Tests.Steps
{
    [Binding]
    public class StepsDefinition
    {
        private HttpClient client;
        private readonly ScenarioContext scenarioContext;
        private readonly string baseUrl = "http://localhost:5000/LineServer/";

        public StepsDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"My server is ready")]
        public async Task SetServerAsync()
        {
            client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}status", baseUrl));
            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<StatusInfo>(content);
            Assert.AreEqual(Status.OK.ToString(), body.State);
        }

        [When(@"I call my line service successfully")]
        public async Task CallServiceAsync()
        {
            var response = await client.GetAsync(string.Format("{0}lines/1", baseUrl));
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            scenarioContext["response"] = response;
            scenarioContext["content"] = content;
        }

        [When(@"I call my line service with invalid index")]
        public async Task CallServiceInvalidAsync()
        {
            var response = await client.GetAsync(string.Format("{0}lines/-1", baseUrl));
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            scenarioContext["response"] = response;
            scenarioContext["content"] = content;
        }

        [When(@"I call my line service with invalid value")]
        public async Task CallServiceInvalidValueAsync()
        {
            var response = await client.GetAsync(string.Format("{0}lines/x", baseUrl));
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            scenarioContext["response"] = response;
            scenarioContext["content"] = content;
        }

        [Then(@"I must retrieve a line")]
        public void RetrieveLine()
        {
            HttpResponseMessage message = (HttpResponseMessage)scenarioContext["response"];
            string content = (string)scenarioContext["content"];
            Assert.IsTrue(message.IsSuccessStatusCode);
            Assert.IsNotEmpty(content);
        }

        [Then(@"I must receive a (.*)")]
        public void RetrieveError(int errorCode)
        {
            HttpResponseMessage message = (HttpResponseMessage)scenarioContext["response"];
            string content = (string)scenarioContext["content"];
            HttpStatusCode statusCode = (HttpStatusCode)errorCode;

            Assert.IsFalse(message.IsSuccessStatusCode);
            Assert.AreEqual(statusCode.ToString(), message.StatusCode.ToString());
        }
    }
}
