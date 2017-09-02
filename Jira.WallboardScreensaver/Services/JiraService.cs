using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Jira.WallboardScreensaver.Services {
    public interface IJiraService {
        Task<IReadOnlyDictionary<string, string>> Login(Uri baseUri, string username, string password);
    }

    public class JiraService : IJiraService {
        public async Task<IReadOnlyDictionary<string, string>> Login(Uri baseUri, string username, string password) {
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
    }
}
