using Aspire.Hosting.Dapr;
using Aspire.Microsoft.Azure.Cosmos;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var stateStore = builder.AddDaprStateStore("statestore");

var apiService = builder.AddProject<Projects.AspireSampleDapr_ApiService>("apiservice")
    .WithDaprSidecar("api")
    .WithReference(stateStore);

builder.AddProject<Projects.AspireSampleDapr_Web>("webfrontend")
    .WithDaprSidecar("web")
    .WithReference(cache);

builder.Build().Run();
