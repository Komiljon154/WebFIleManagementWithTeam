
using WebFileManagement.Service.Services;
using WebFileManagement.StorageBroker.Services;

namespace WebFileManagement.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IStorageService, StorageService>(); // Har bitta userdan kegan request uchun object qilib beradi
            builder.Services.AddSingleton<IStorageBrokerService, LocalStorageBrokerService>(); // faqat bitta object yasaydi
            //builder.Services.AddSingleton<IStorageBrokerService, AwsStorageService>(); // faqat bitta object yasaydi 
            //builder.Services.AddSingleton<IStorageService, StorageService>(); // Bitta object yasaydi hamma uchun ishlatadi
            //builder.Services.AddTransient<IStorageService, StorageService>(); // Xar biita reques uchun yangi object yasaydi

            // lose coupling and easy to switch between objects

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

            app.Run();
        }
    }
}
