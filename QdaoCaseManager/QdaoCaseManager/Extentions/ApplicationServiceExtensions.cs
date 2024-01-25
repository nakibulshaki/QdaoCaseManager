using Microsoft.AspNetCore.Identity;
using QdaoCaseManager.Infrastructure.Data;
using QdaoCaseManager.Repositories.Notes;
using QdaoCaseManager.Services.Cases;
using QdaoCaseManager.Services.Email;
using QdaoCaseManager.Services.Notes;

namespace QdaoCaseManager.Extentions;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //DI Register Here
        services.AddScoped<ICaseAppService, CaseAppService>();
        services.AddScoped<INoteAppService, NoteAppService>();
        services.AddScoped<INoteRepository, NoteRepository>();

        return services;
    }
}
