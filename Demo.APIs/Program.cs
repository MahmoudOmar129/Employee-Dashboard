using Demo.BL.Interfaces;
using Demo.BL.Mapper;
using Demo.BL.Repository;
using Demo.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace Demo.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddControllers()
                .AddXmlSerializerFormatters();
            //.AddXmlDataContractSerializerFormatters();
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //});



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region Connection String Service

            // Enhancement ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

            builder.Services.AddDbContextPool<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

            #endregion

            #region Auto Mapper

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            #endregion

            builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();

            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(options => options
                 .WithOrigins() // Server
                 .AllowAnyMethod() // Verbs
                 .AllowAnyHeader()); // Format (XML , JSON)

            app.Run();
        }
    }
}