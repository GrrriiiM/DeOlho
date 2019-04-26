using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DeOlho.ETL.Sources
{
    public class HttpJsonSource : Source<string>
    {
        readonly string _uri;
        readonly dynamic uriParameters;
        public HttpJsonSource(string uri, dynamic uriParameters = null)
        {
            this._uri = uri;
            this.uriParameters = uriParameters;
        }

        public async override Task<string> Execute()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(this._uri);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}