var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.ProjectName_ApiService>("api");
var web = builder.AddProject<Projects.ProjectName_Web>("web")
    .WithReference(api)
    .WaitFor(api);


builder.Build().Run();
