Seq HTTP API Client
===================

[![Build status](https://ci.appveyor.com/api/projects/status/58i2tne3ga4jcx5s?svg=true)](https://ci.appveyor.com/project/NicholasBlumhardt/seq-api)

**To raise issues** with this library please use the [issue tracker here](https://github.com/continuousit/seq-releases/issues).

This library includes:

 * C# representations of the entities exposed by the Seq HTTP API
 * Helper classes for interacting with the API

It's useful for querying events and working with configuration data - *everything you can do using the Seq web UI*, you can do programmatically via the API.

If you want to *write events* to Seq, use one of the logging framework clients, such as _Serilog.Sinks.Seq_ or _Seq.Client.Slab_ instead.

Getting Started
---------------

Install from NuGet:

```powershell
Install-Package Seq.Api
```

Create a `SeqConnection` with your server URL:

```csharp
var connection = new SeqConnection("http://localhost:5341");
```

Navigate the "resource groups" exposed as properties of the `connnection`:

```csharp
var installedApps = await connection.Apps.ListAsync();
```

**To authenticate**, the `SeqConnection` constructor accepts an `apiKey` parameter (make sure the API key permits _user-level access_) or, if you want to log in with personal credentials you can `await connection.Users.Login(username, password)`.

For a more complete example, see the [seq-tail app included in the source](https://github.com/continuousit/seq-api/blob/master/example/SeqTail/Program.cs);

Working with the Basic Client
-----------------------------

The `SeqApiClient` class implements the low level interactions with the API's entities and links. It's one step up from `System.Net.HttpClient` - you may be able to use it in cases not supported by the high-level wrapper. 

Create a `SeqApiClient` with your server URL:

```csharp
var client = new SeqApiClient("http://localhost:5341");
```

Get the root resource and use it to retrieve one or more of the resource groups:

```csharp
var root = client.GetRootAsync();
var events = await client.GetAsync<ResourceGroup>(root, "EventsResources");
```

(Available resource groups, like `events`, `users` and so-on, can be seen in the root document's `Links` collection.)

Use the client to navigate links from entity to entity:

```csharp
var matched = await client.List<EventEntity>(
  events,
  "Items",
  new Dictionary<string, object>{{"count", 10}, {"render", true}});

foreach (var match in matched)
  Console.WriteLine(matched.RenderedMessage);
```

Status
------

This library is under active development.

* The entity types etc. are complete: they're the same ones Seq uses internally.
* The helper classes such as `SeqConnection` and `SeqApiClient` are evolving and may change in response to feedback (and PRs!).
