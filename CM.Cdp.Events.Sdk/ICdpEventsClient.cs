using CM.Cdp.Events.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Cdp.Events.Sdk
{
    public interface ICdpEventsClient
    {
        /// <summary>
        /// Add new events
        /// </summary>
        /// <param name="tenantId">Your Tenant Id, used for identification. (Found in your CDP app under "settings")</param>
        /// <param name="events">List of events you wish to add.</param>
        /// <param name="cancellationToken">An optional cancellation token to abort this request.</param>
        /// <returns></returns>
        Task AddEvents(Guid tenantId, IEnumerable<ApiEvent> events, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add new whitelisted events. All events should have been whitelisted, and do not need an ApiKey for authentication.
        /// </summary>
        /// <param name="tenantId">Your Tenant Id, used for identification. (Found in your CDP app under "settings")</param>
        /// <param name="events">List of events you wish to add.</param>
        /// <param name="cancellationToken">An optional cancellation token to abort this request.</param>
        /// <returns></returns>
        Task AddAnonymousEvents(Guid tenantId, IEnumerable<ApiEvent> events, CancellationToken cancellationToken = default);
    }
}
