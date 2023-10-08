
using CapitalPlacement.Abstracts;
using CapitalPlacement.DataLayer;
using CapitalPlacement.DataTransferModels;
using CapitalPlacement.Services;

namespace CapitalPlacement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<DBSettings>(builder.Configuration.GetSection(nameof(DBSettings)));
            builder.Services.AddSingleton<DBContext>();
            builder.Services.AddScoped<IProgramService, ProgramService>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddScoped<IWorkflowService, WorkflowService>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}