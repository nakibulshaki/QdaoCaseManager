using Microsoft.Extensions.DependencyInjection;

namespace QdaoCaseManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //DI Register Here
        services.AddScoped<ICaseAppService, CaseAppService>();
        services.AddScoped<INoteAppService, NoteAppService>();
        return services;
    }

}

