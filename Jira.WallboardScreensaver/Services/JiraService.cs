using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

using JiraCredentials = System.Collections.Generic.IReadOnlyDictionary<string, string>;

namespace Jira.WallboardScreensaver.Services {
    public class JiraDashboard {
        public JiraDashboard(string name, int id) {
            Name = name;
            Id = id;
        }
        public string Name { get; }

        public int Id { get; }
    }

    public interface IJiraService {
        Task<JiraCredentials> LoginAsync(Uri baseUri, string username, string password);
        Task<IEnumerable<JiraDashboard>> GetDashboardsAsync(Uri baseUri, JiraCredentials credentials);
    }

    public class JiraService : IJiraService {
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

        public async Task<IEnumerable<JiraDashboard>> GetDashboardsAsync(Uri baseUri, JiraCredentials credentials) {
            throw new NotImplementedException();
        }
    }
}
