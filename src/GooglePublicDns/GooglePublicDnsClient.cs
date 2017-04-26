using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GooglePublicDns
{
    public class GooglePublicDnsClient
    {
        public const string BaseUrl = "https://dns.google.com/resolve?";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="cd"></param>
        /// <param name="ednsClientSubnet"></param>
        /// <param name="randomPadding"></param>
        /// <returns></returns>
        public async Task<GooglePublicDnsResponse> Resolve(string name, RecordType? type = null, bool cd = false, string ednsClientSubnet = "", string randomPadding = "")
        {
            var client = new HttpClient();

            var query = "name=" + Uri.EscapeDataString(name);

            if (type.HasValue)
                query += "&type=" + type;

            if (cd)
                query += "&cd=true";

            if (!string.IsNullOrEmpty(ednsClientSubnet))
            {
                query += "&edns_client_subnet=" + ednsClientSubnet;
            }

            if (!string.IsNullOrEmpty(randomPadding))
            {
                query += "&random_padding=" + randomPadding;
            }

            HttpResponseMessage response = await client.GetAsync(BaseUrl + query);

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var dnsResponse = JsonConvert.DeserializeObject<GooglePublicDnsResponse>(body);
                dnsResponse.RawJson = body;

                return dnsResponse;
            }

            return null;
        }
    }
}
