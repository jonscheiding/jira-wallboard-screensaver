using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using HttpMock;
using Jira.WallboardScreensaver.Services;
using NSubstitute;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class JiraServiceTests {
        private static readonly Dictionary<string, string> NoCookies = new Dictionary<string, string>();

        private Uri _baseUri;
        private IHttpServer _http;
        private JiraService _jiraService;
        private JavaScriptSerializer _serializer;

        [SetUp]
        public void SetUp() {
            _baseUri = new Uri("http://localhost:9919");
            _http = HttpMockRepository.At(_baseUri);
            _jiraService = new JiraService();
            _serializer = new JavaScriptSerializer();
        }

        [Test]
        public async Task PostsCredentialsToLoginEndpoint() {
            var handler = (RequestHandler)_http.Stub(x => x.Post("/rest/auth/1/session"));
            handler.OK();

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
            _http.Stub(x => x.Post("/rest/api/2/dashboard"))
                .WithStatus(HttpStatusCode.Unauthorized);

            //

            Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _jiraService.LoginAsync(_baseUri, "", ""));

            Assert.ThrowsAsync<HttpRequestException>(async () =>
                await _jiraService.GetDashboardsAsync(_baseUri, NoCookies));
        }

        [Test]
        public async Task PassesCookiesWhenGettingDashboards() {
            var handler = (RequestHandler)_http.Stub(x => x.Get("/rest/api/2/dashboard"));
            handler.Return("{dashboards: []}").OK();

            var cookies = new Dictionary<string, string> {
                {"cookie1", "value1"},
                {"cookie2", "value2"}
            };

            //

            await _jiraService.GetDashboardsAsync(_baseUri, cookies);

            //

            var request = handler.LastRequest();

            Assert.That(request.RequestHead.Headers, Contains.Key("Cookie"));
            Assert.That(request.RequestHead.Headers["Cookie"], Is.EqualTo("cookie1=value1; cookie2=value2"));
        }

        [Test]
        public async Task ParsesAndReturnsInformationAboutReturnedDashboards() {
            var dashboardsResponse = new {
                dashboards = CreateDashboardsResponseData("10000", "20000")
            };

            _http.Stub(x => x.Get("/rest/api/2/dashboard"))
                .Return(_serializer.Serialize(dashboardsResponse))
                .OK();

            //

            var results = await _jiraService.GetDashboardsAsync(_baseUri, NoCookies);

            //

            Assert.That(results, Is.EquivalentTo(new [] {
                new JiraDashboard("Dashboard 10000", 10000),
                new JiraDashboard("Dashboard 20000", 20000)  
            }));
        }

        [Test]
        public async Task FollowsPaginationLinksForDashboardsIfTheyExist() {
            var dashboardsResponse1 = new {
                dashboards = CreateDashboardsResponseData("10000"),
                next = new Uri(_baseUri, "/page2").ToString()
            };
            var dashboardsResponse2 = new {
                dashboards = CreateDashboardsResponseData("20000"),
                next = new Uri(_baseUri, "/page3").ToString()
            };
            var dashboardsResponse3 = new {
                dashboards = CreateDashboardsResponseData("30000")
            };

            _http.Stub(x => x.Get("/rest/api/2/dashboard"))
                .Return(_serializer.Serialize(dashboardsResponse1)).OK();
            _http.Stub(x => x.Get("/page2"))
                .Return(_serializer.Serialize(dashboardsResponse2)).OK();
            _http.Stub(x => x.Get("/page3"))
                .Return(_serializer.Serialize(dashboardsResponse3)).OK();

            //

            var results = await _jiraService.GetDashboardsAsync(_baseUri, NoCookies);

            //

            Assert.That(results, Is.EquivalentTo(new[] {
                new JiraDashboard("Dashboard 10000", 10000),
                new JiraDashboard("Dashboard 20000", 20000),
                new JiraDashboard("Dashboard 30000", 30000)
            }));
        }

        private dynamic[] CreateDashboardsResponseData(params string[] dashboardIds) {
            return dashboardIds.Select(id => new {
                id,
                name = $"Dashboard {id}",
                self = new Uri(_baseUri, $"/rest/api/2/dashboard/{id}"),
                view = new Uri(_baseUri, $"/secure/Dashboard.jspa?selectPageId={id}")
            }).ToArray();
        }
    }
}
