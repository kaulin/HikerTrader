using Microsoft.Extensions.DependencyInjection;
using HikerTrader.Sources.Services;
using HikerTrader.Sources.Data;

class Program
{
    static void Main()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var hikerService = serviceProvider.GetRequiredService<IHikerService>();
        hikerService.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IHikerRepository, HikerRepository>();
        services.AddSingleton<IItemRepository, ItemRepository>();
        services.AddScoped<IHikerService, HikerService>();
    }
}