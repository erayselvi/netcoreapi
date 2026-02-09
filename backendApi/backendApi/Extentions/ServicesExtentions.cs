using Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
namespace backendApi.Extentions
{
    public static class ServicesExtentions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,//genişletilmek istenen sınıf this ile ifade edilir.
            IConfiguration configuration) =>        //lambda
            services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
            );

    }
}
