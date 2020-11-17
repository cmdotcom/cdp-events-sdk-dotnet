using CM.Cdp.Events.Sdk.Exceptions;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Cdp.Events.Sdk
{
    public abstract class BaseClient
    {
        private const string apiKeyHeader = "X-CM-PRODUCTTOKEN";
        private const string jsonMediaType = "application/json";
        private readonly Encoding encoding = Encoding.UTF8;

        private readonly HttpClient _httpClient;
        private readonly Guid? _apiKey;
        private readonly string _baseUrl;

        public BaseClient(HttpClient httpClient, Guid? apiKey, string baseUrl)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Performs a REST POST request.
        /// </summary>
        /// <param name="url">The url extension for the endpoint</param>
        /// <param name="data">The object that will be send with your request</param>
        /// <param name="cancellationToken">An optional cancellation token to abort this request</param>
        /// <returns>The returned response</returns>
        protected async Task Post([NotNull] string url, [NotNull] object data, CancellationToken cancellationToken)
        {
            if (_apiKey.HasValue == false || _apiKey.Value == Guid.Empty)
            {
                throw new ApiMissingApiKeyException($"Missing or incorrect Api key.");
            }

            using var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}{url}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), encoding, jsonMediaType)
            };
            request.Headers.Add(apiKeyHeader, _apiKey.Value.ToString());

            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            await EnsureSuccessStatusCodeAsync(response).ConfigureAwait(false);
        }

        /// <summary>
        /// Performs a REST POST request, withoud adding any security headers.
        /// </summary>
        /// <param name="url">The url extension for the endpoint</param>
        /// <param name="data">The object that will be send with your request</param>
        /// <param name="cancellationToken">An optional cancellation token to abort this request</param>
        /// <returns>The returned response</returns>
        protected async Task PostUnauthenticated([NotNull] string url, [NotNull] object data, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}{url}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), encoding, jsonMediaType)
            };

            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            await EnsureSuccessStatusCodeAsync(response).ConfigureAwait(false);
        }


        private async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            if (response.Content != null)
            {
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.Content.Dispose();
                throw new ApiResponseException(response.StatusCode, content, response.ReasonPhrase);
            }
        }
    }
}
