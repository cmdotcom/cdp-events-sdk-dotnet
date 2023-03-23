using CM.Cdp.Events.Sdk.Models;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Cdp.Events.Sdk
{
    /// <summary>
    /// This clients provides all options to send new events into the CDP Environment.
    /// </summary>
    [PublicAPI]
    public class CdpEventsClient : BaseClient, ICdpEventsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CdpEventsClient" /> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient used for sending communicating with CDP. Generally, you should use only one version within your application.</param>
        /// <param name="apiKey">Your Product token, used for authentication. (Found in your CDP app under "settings", optional)</param>
        /// <param name="baseUrl">The base URL of the CM CDP API (Optional)</param>
        public CdpEventsClient(HttpClient httpClient, Guid? apiKey, string baseUrl = "https://api.cdp.cm.com") : base(httpClient, apiKey, baseUrl) { }

        /// <summary>
        /// Add new events
        /// </summary>
        /// <param name="tenantId">Your Tenant Id, used for identification. (Found in your CDP app under "settings")</param>
        /// <param name="events">List of events you wish to add.</param>
        /// <param name="cancellationToken">An optional cancellation token to abort this request.</param>
        /// <returns></returns>
        public async Task AddEvents(Guid tenantId, IEnumerable<ApiEvent> events, CancellationToken cancellationToken = default)
            => await Post($"/events/v1.0/tenants/{tenantId}/events", events, cancellationToken);

        /// <summary>
        /// Add new whitelisted events. All events should have been whitelisted, and do not need an ApiKey for authentication.
        /// </summary>
        /// <param name="tenantId">Your Tenant Id, used for identification. (Found in your CDP app under "settings")</param>
        /// <param name="events">List of events you wish to add.</param>
        /// <param name="cancellationToken">An optional cancellation token to abort this request.</param>
        /// <returns></returns>
        public async Task AddAnonymousEvents(Guid tenantId, IEnumerable<ApiEvent> events, CancellationToken cancellationToken = default)
            => await PostUnauthenticated($"/events/v1.0/tenants/{tenantId}/events/anonymousevents", events, cancellationToken);

    }
}
