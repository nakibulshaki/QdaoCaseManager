using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QdaoCaseManager.Application.Cases;
using QdaoCaseManager.Application.Email;
using QdaoCaseManager.Infrastructure.identity;
using QdaoCaseManager.Services.Notes;

namespace QdaoCaseManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Enable when you have access
        //builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        services.Configure<AuthMessageSenderOptions>(configuration);

        //DI Register Here
        services.AddScoped<ICaseAppService, CaseAppService>();
        services.AddScoped<INoteAppService, NoteAppService>();
        return services;
    }

}

