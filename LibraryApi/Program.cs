using LibraryApi.DataAccess.EFRepository;
using LibraryApi.DataAccess.Interface;
using LibraryApi.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LibraryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("PostgressConnection");

            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

            builder.Services.AddScoped<IBookRepository, BookRepository>()
                            .AddScoped<IAuthorRepository, AuthorRepository>();


            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new PatchRequestContractResolver();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
