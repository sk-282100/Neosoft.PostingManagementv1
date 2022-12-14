using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PostingManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IExcelUploadRepository, ExcelUploadRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IEmployeeTransferRepository, EmployeeTransferRepository>();
            services.AddScoped<IJobFamilyRepository, JobFamilyRepository>();
            services.AddScoped<ITriggerRepository, TriggerRepository>();
            services.AddScoped<IScaleRepository, ScaleRepository>();

            return services;
        }
    }
}
