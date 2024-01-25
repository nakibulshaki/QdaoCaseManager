using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QdaoCaseManager.Infrastructure.Data;
using QdaoCaseManager.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Infrastructure;

    public static class DependencyInjection
    {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        DataValidation.GuardAgainstNullString(connectionString, message: "Connection string 'DefaultConnection' not found.");


        return services;
    }

}

