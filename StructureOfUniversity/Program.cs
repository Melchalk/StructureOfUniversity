using StructureOfUniversity;

Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            webBuilder.UseWebRoot("Static");
        })
    .Build()
    .Run();
