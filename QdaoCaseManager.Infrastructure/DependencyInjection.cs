using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QdaoCaseManager.Infrastructure.Data;
using QdaoCaseManager.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QdaoCaseManager.Infrastructure.identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QdaoCaseManager.Infrastructure.Repositories;
using QdaoCaseManager.Domain.Repositories;

namespace QdaoCaseManager.Infrastructure;

public static class DependencyInjection
    {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        DataValidation.GuardAgainstNullString(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<ICaseRepository, CaseRepository>();
        return services;
    }

}

