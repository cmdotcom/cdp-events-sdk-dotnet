# CDP EventsAPI SDK
.NET Standard SDK to send email with the CM CDP Events API

## API Documentation
https://docs.cmtelecom.com/en/api/cdp-events/

# Usage

## Instantiate a client

```cs
HttpClient httpClient = new HttpClient();
CdpEventsClient eventsClient = new CdpEventsClient(httpClient, myApiKey);
```

`httpClient` is requested as a parameter, such that you can use a single instance throughout your application, as is highly recommended.

`myApiKey` is your unique api key (or product token) which authorizes you on the CM platform. Always keep this key secret!

`baseUrl` is optional and will be defaulted to https://api.cm.com when not filled in. Else the requests will be send to the filled in domain.

## Send an event

You can send events by calling the `AddEvents` method and providing your tenantID (found in the settings page within your Cdp app), a list of events and a cancellation token (optional).

Each event should match a corresponding event type, in order for Cdp to correctly interpret your message. The event type Id's can be found in the sources page in your Cdp app.

```cs
ApiEvent[] events = new[]
{
	new ApiEvent
	{
		EventTypeId = registrationEventTypeId,
		Event = new Registration
		{
			Email = userEmailAddress,
			Message = "It's a great day",
			Subject = "The sun is shining and so are you."
		}
	},
	new ApiEvent
	{
		EventTypeId = crmUpdateEventTypeId,
		Event = new CrmUpdate
		{
			Email = userEmailAddress,
			PhoneNumber = userPhoneNumber
		}
	}
};
await eventsClient.AddEvents(myTenantId, events).ConfigureAwait(false);
```

## Send an anonymous event

Some event types can be whitelisted, so that no authentication is needed for sending the events. This may be useful for sending tracking data from websites. When calling the `AddAnonymousEvents` method, your ApiKey will not be validated, only if the event type is whitelisted for your account.

```cs
ApiEvent[] events = new[]
{
	new ApiEvent
	{
		EventTypeId = memberPageVisitEventTypeId,
		Event = new PageVisit
		{
			Email = userEmailAddress,
			Page = currentPageUrl
		}
	}
};
await eventsClient.AddAnonymousEvents(myTenantId, events).ConfigureAwait(false);
```