using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace TestGitHubApi
{
    public class Github_Tests
    {
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient("https://api.github.com");
            client.Authenticator = new HttpBasicAuthenticator("USER", "PASSWORD/TOKEN");

            string url = "/repos/VeniaminVasilev/Postman/issues";
            this.request = new RestRequest(url);
        }

        [Test]
        public async Task Test_Get_Issue()
        {
            var response = await client.ExecuteAsync(request);

            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

            foreach (var issue in issues)
            {
                Assert.IsNotNull(issue.html_url);
                Assert.IsNotNull(issue.id, "Issue id must not be null");
            }

            Assert.IsNotNull(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}