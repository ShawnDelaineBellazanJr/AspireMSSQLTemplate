var builder = DistributedApplication.CreateBuilder(args);




var sqlServer = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent)
                 .AddDatabase("db");

var api = builder.AddProject<Projects.ProjectName_ApiService>("api")
    .WithReference(sqlServer)
    .WaitFor(sqlServer);


var web = builder.AddProject<Projects.ProjectName_Web>("web")
    .WithReference(api)
    .WaitFor(api);


builder.Build().Run();
