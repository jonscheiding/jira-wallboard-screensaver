using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpMock;
using Jira.WallboardScreensaver.Services;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class JiraServiceTests {
        private Uri _baseUri;
        private IHttpServer _http;
        private JiraService _jiraService;

        [SetUp]
        public void SetUp() {
            _baseUri = new Uri("http://localhost:9919");
            _http = HttpMockRepository.At(_baseUri);
            _jiraService = new JiraService();
        }

        [Test]
        public async Task PostsCredentialsToLoginEndpoint() {
            RequestHandler handler = null;
            _http.Stub(x => handler = x.Post("/rest/auth/1/session"))
                .OK();

            //

            await _jiraService.LoginAsync(_baseUri, "user", "pass");
            
            //

            Assert.That(handler.GetBody(), Is.EqualTo("{\"username\":\"user\",\"password\":\"pass\"}"));
        }

        [Test]
        public async Task ReturnsCookiesAsDictionary() {
            _http.Stub(x => x.Post("/rest/auth/1/session"))
                .AddHeader("Set-Cookie", "cookie1=value1; Path=/, cookie2=value2; Path=/")
                .OK();

            //

            var result = await _jiraService.LoginAsync(_baseUri, "", "");

            //

            Assert.That(result, Is.EquivalentTo(new Dictionary<string, string>
            {
                { "cookie1", "value1" },
                { "cookie2", "value2" }
            }));
        }

        [Test]
        public void ThrowsHttpRequestExceptionOnUnauthorized() {
            _http.Stub(x => x.Post("/rest/auth/1/session"))
                .WithStatus(HttpStatusCode.Unauthorized);

            //

            Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _jiraService.LoginAsync(_baseUri, "", ""));
        }
    }
}
