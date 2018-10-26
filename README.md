# SharpApiRateLimit

Super simple API throttling / rate limit framework based on attributes

## How to Use

Install the [SharpApiRateLimit package](https://www.nuget.org/packages/SharpApiRateLimit/) via Nuget:

```powershell
Install-Package SharpApiRateLimit
```

Open your *Startup.cs* and configure your reverse proxy:

```csharp
public void ConfigureServices(IServiceCollection services) {
    services.AddRateLimit();
    //other
    ...
}
```

Add your attributes on a controller or action level:

```csharp

[RateLimitByHeader("X-UserId", "1s", calls: 1)]
public class ValuesController : ControllerBase {

    [HttpGet]
    [RateLimitByHeader("X-UserId", "15s", calls: 2)]
    public ActionResult<IEnumerable<string>> Get() {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    [RateLimitByHeader("X-UserId", "10m", calls: 100)]
    public ActionResult<string> Get(int id) {
        return "value";
    }

    [HttpPost]
    public void Post([FromBody] string value) {
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) {
    }

    [HttpDelete("{id}")]
    public void Delete(int id) {
    }
}
```

In the example above, all calls will be **rate limited to 1 call per second**. Users will be identified using the header `'X-UserId'`, use the one you need.

The limits for the actions `GET:/values/get` and `GET:/values/get/{id}` will be overridden with the local attribute.

### Valid periods:
- s: seconds. Ex: 1s, 4s
- m: minutes. Ex: 10m, 30m
- h: hours. Ex: 1h, 10h
- d: days. Ex. 1d, 2d

## Customize your storage:

Information about current calls are stored using Asp.Net `IMemoryCache`. If you want to change that, just create a class that implements `IRateLimitStorage` and register it with the ServiceLocator.

Ex:             
```
services.AddSingleton<IRateLimitStorage, MyCustomStorage>();
```


## Customize your 429 message response

To change the error message, create a class implementing `IRateLimitResponseSerializer` and register it:  

Ex:             
```
services.AddSingleton<IRateLimitResponseSerializer, MyCustomResponseSerializer>();
```

Enjoy!