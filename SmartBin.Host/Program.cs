
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SmartBin.Api"))
        );

        services.AddHostedService<HostWorker>();
        services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttOptions"));
        services.AddSingleton<ManagedMqttClient>();
        services.AddSingleton<MqttBuffer>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPointChangeService, PointChangeService>();
        services.AddScoped<IPointChangeRepository, PointChangeRepository>();
        services.AddScoped<IBinService, BinService>();
        services.AddScoped<IBinRepository, BinRepository>();
        services.AddScoped<IBinUnitService, BinUnitService>();
        services.AddScoped<IBinUnitRepository, BinUnitRepository>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAdminRepository, AdminRepository>();

        services.AddAutoMapper(typeof(ModelToViewModelProfile));
        services.AddAutoMapper(typeof(ViewModelToModelProfile));
    })
    .Build();

await host.RunAsync();
