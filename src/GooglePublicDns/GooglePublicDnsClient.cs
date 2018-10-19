using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GooglePublicDns
{
    public class GooglePublicDnsClient
    {
        private static HttpClient _client;

        /// <summary>
        /// Instantiate client
        /// </summary>
        /// <param name="timeout">Timespan to wait before the request times out (miliseconds)</param>
        public GooglePublicDnsClient(long timeout = 1000)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://dns.google.com/"),
                Timeout = TimeSpan.FromMilliseconds(timeout)
            };
        }

        /// <summary>
        /// Resolve DNS entry
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="cd"></param>
        /// <param name="ednsClientSubnet"></param>
        /// <param name="randomPadding"></param>
        /// <returns></returns>
        public async Task<GooglePublicDnsResponse> Resolve(string name, RecordType? type = null, bool cd = false, string ednsClientSubnet = "", string randomPadding = "")
        {
            try
            {
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

                HttpResponseMessage response = await _client.GetAsync($"resolve?{query}").ConfigureAwait(false);

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
            catch (TaskCanceledException)
            {
                // timeout
                throw new TimeoutException();
            }
        }
    }
}
