# HackerNews
RESTful API to retrieve the details of the first 20 "best stories" from the Hacker News API in ASP.NET Core 2.2



## How to run

1. Open solution in Visual Studio;
2. Press F5 to run the application;
3. Use the swagger interface to request best stories;



## Assumptions

1. Design pattern DDD (domain-driven design);
2. RestSharp;
3. AutoMapper;
4. Parallel and ConcurrentBag to improve the performance;
5. SWAGGER;
6. **AspNetCoreRateLimit**: Limit of 100 requests per minute configured in appsettings.json;
7. **MemoryCache**: Local cache to improve the performance;



## Future Enhancements

1. Distributed cache using Redis;
