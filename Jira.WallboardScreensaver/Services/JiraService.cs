using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Jira.WallboardScreensaver.EditPreferences;
using JiraCredentials = System.Collections.Generic.IReadOnlyDictionary<string, string>;
using IDashboardDisplayItem2 = Jira.WallboardScreensaver.EditPreferences2.IDashboardDisplayItem;

namespace Jira.WallboardScreensaver.Services {
    public class JiraDashboard : IDashboardDisplayItem, IDashboardDisplayItem2 {
        public JiraDashboard(string name, int id) {
            Name = name;
            Id = id;
        }
        public string Name { get; }

        public int Id { get; }

        public override string ToString() {
            return Name;
        }

        public override int GetHashCode() {
            return $"{Id}-{Name}".GetHashCode();
        }

        public override bool Equals(object obj) {
            var objAs = obj as JiraDashboard;
            return objAs != null && objAs.Id == Id && objAs.Name == Name;
        }
    }

    public interface IJiraService {
        Task<JiraCredentials> LoginAsync(Uri baseUri, string username, string password);
        Task<IEnumerable<JiraDashboard>> GetDashboardsAsync(Uri baseUri, JiraCredentials credentials);
    }

    public class JiraService : IJiraService {
        private readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

#pragma warning disable 649
        // ReSharper disable InconsistentNaming
        // ReSharper disable ClassNeverInstantiated.Local
        private class DashboardsResponseData {
            public DashboardResponseData[] dashboards;
            public string next;
        }

        private class DashboardResponseData {
            public string id;
            public string name;
        }

        // ReSharper restore InconsistentNaming
        // ReSharper restore ClassNeverInstantiated.Local
#pragma warning restore 649

        public async Task<JiraCredentials> LoginAsync(Uri baseUri, string username, string password) {
            var cookies = new CookieContainer();
            var client = new HttpClient(new HttpClientHandler { CookieContainer = cookies }) { BaseAddress = baseUri };
            var serializer = new JavaScriptSerializer();

            var credentials = new { username, password };
            var content = serializer.Serialize(credentials);

            var result = await client.PostAsync("/rest/auth/1/session", new StringContent(content, Encoding.Default, "application/json"));

            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Authentication failed with status {result.StatusCode}.");
            }

            return cookies.GetCookies(baseUri)
                .Cast<Cookie>()
                .ToDictionary(c => c.Name, c => c.Value);
        }

        public Task<IEnumerable<JiraDashboard>> GetDashboardsAsync(Uri baseUri, JiraCredentials credentials) {
            return GetDashboardsAsync(baseUri, "/rest/api/2/dashboard", credentials);
        }

        private async Task<IEnumerable<JiraDashboard>> GetDashboardsAsync(Uri baseUri, string path,
            JiraCredentials credentials) {

            var cookies = new CookieContainer();
            var client = new HttpClient(new HttpClientHandler { CookieContainer = cookies }) { BaseAddress = baseUri };

            foreach (var kvCookie in credentials) {
                cookies.Add(baseUri, new Cookie(kvCookie.Key, kvCookie.Value));
            }

            var result = await client.GetAsync(path);

            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"Authentication failed with status {result.StatusCode}.");
            }

            var content = await result.Content.ReadAsStringAsync();
            var data = _serializer.Deserialize<DashboardsResponseData>(content);

            var results = data.dashboards
                .Select(dashboard => new JiraDashboard(dashboard.name, int.Parse(dashboard.id)));

            if (data.next != null) {
                results = results.Concat(await GetDashboardsAsync(baseUri, data.next, credentials));
            }

            return results;
        }
    }
}
