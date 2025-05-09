# Seq HTTP API Client [![Build status](https://ci.appveyor.com/api/projects/status/bhtx25hyqmmdqhvt?svg=true)](https://ci.appveyor.com/project/datalust/seq-api) [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Seq.Api.svg)](https://nuget.org/packages/seq.api)

This library includes:

 * C# representations of the entities exposed by the Seq HTTP API
 * Helper classes for interacting with the API

It's useful for querying events and working with configuration data - *everything you can do using the Seq web UI*, you can do programmatically via the API.

If you want to *write events* to Seq, use one of the logging framework clients, such as _Serilog.Sinks.Seq_ or _NLog.Targets.Seq_ instead.

### Getting started

Install from NuGet:

```powershell
dotnet add package Seq.Api
```

Create a `SeqConnection` with your server URL:

```csharp
var connection = new SeqConnection("http://localhost:5341");
```

Navigate the "resource groups" exposed as properties of the `connnection`:

```csharp
var installedApps = await connection.Apps.ListAsync();
```

**To authenticate**, the `SeqConnection` constructor accepts an `apiKey` parameter (make sure the API key permits _user-level access_) or, if you want to log in with personal credentials you can `await connection.Users.LoginAsync(username, password)`.

For a more complete example, see the [seq-tail app included in the source](https://github.com/datalust/seq-api/blob/main/example/SeqTail/Program.cs).

#### Creating entities

The Seq API provides a `/template` resource for each resource group that provides a new instance of the resource with defaults populated. The API client uses this pattern when creating new entities:

```csharp
var signal = await connection.Signals.TemplateAsync();
signal.Title = "Signal 123";
await connection.Signals.AddAsync(signal);
```

See the [signal-copy app](https://github.com/datalust/seq-api/blob/main/example/SignalCopy/Program.cs) for an example of this pattern in action.

### Reading events

Seq internally limits the resources a query is allowed to consume. The query methods on `SeqConnection.Events` include a _status_ with each result set - a `Partial` status indicates that further results must be retrieved.

The snippet below demonstrates lazily enumerating through results to retrieve the complete set.

```csharp
var resultSet = connection.Events.EnumerateAsync(
    filter: "Environment = 'Test'",
    render: true,
    count: 1000);

await foreach (var evt in resultSet)
  Console.WriteLine(evt.RenderedMessage);
```

All methods that retrieve events require a `count`. The API client defaults this value to `30` if not specified.

### Streaming events

Seq provides live streaming of events matching a filter and/or set of signals.

```csharp
var filter = "@Level = 'Error'";

await foreach (var evt in connection.Events.StreamAsync<JObject>(filter: filter, clef: true))
{
    var logEvent = LogEventReader.ReadFromString(evt);
    Log.Write(logEvent);
}
```

`Events.StreamAsync()` method returns `IAsyncEnumerable<T>` over a _WebSocket_. The enumerator will keep producing events until either it's disposed, or the server is shut down.

Seq streams the events in [compact JSON format](https://github.com/serilog/serilog-formatting-compact), which the Seq API client library can deserialize into JSON.NET `JObjects` for consumption.

[_Serilog.Formatting.Compact.Reader_](https://github.com/serilog/serilog-formatting-compact-reader) provides the `LogEventReader` class used above to turn these documents back into Serilog `LogEvent`s. Having the events represented in Serilog’s object model means they can be passed back into a logging pipeline, as performed above using `Log.Write()`.

### Working with the basic client

The `SeqApiClient` class implements the low level interactions with the API's entities and links. It's one step up from `System.Net.HttpClient` - you may be able to use it in cases not supported by the high-level wrapper. 

Create a `SeqApiClient` with your server URL:

```csharp
var client = new SeqApiClient("http://localhost:5341");
```

Get the root resource and use it to retrieve one or more of the resource groups:

```csharp
var root = await client.GetRootAsync();
var events = await client.GetAsync<ResourceGroup>(root, "EventsResources");
```

(Available resource groups, like `Events`, `Users` and so-on, can be seen in the root document's `Links` collection.)

Use the client to navigate links from entity to entity:

```csharp
var matched = await client.ListAsync<EventEntity>(
  events,
  "Items",
  new Dictionary<string, object>{{"count", 10}, {"render", true}});

foreach (var match in matched)
  Console.WriteLine(match.RenderedMessage);
```

### Package versioning

This package does not follow the SemVer rule of major version increments for breaking changes. Instead, the package version tracks the Seq version it supports.
