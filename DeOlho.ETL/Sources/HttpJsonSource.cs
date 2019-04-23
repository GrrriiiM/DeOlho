using System.Net.Http;
using System.Net.Http.Headers;

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

        public override string Execute()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(this._uri).Result;

                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}