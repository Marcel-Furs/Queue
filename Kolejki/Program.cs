using Kolejki.API.Services;
using Kolejki.ApplicationCore.Models;
using Kolejki.ApplicationCore.Repositories;
using Kolejki.ApplicationCore.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

public partial class Program {
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var connectionString = builder.Configuration.GetConnectionString("Default");            //do bazy danych
                                                                                                //builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));      //do bazy danych

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IMQTTService, MQTTService>();

        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IRepository<Payment>, PaymentRepository>();
        builder.Services.AddScoped<IRepository<PaymentStatus>, PaymentStatusRepository>();

        builder.Services.AddScoped<IPaypalService, PaypalService>();

        builder.Services.AddHttpClient("GetToken", c =>
        {
            c.BaseAddress = new Uri(builder.Configuration["Paypal:Urls:GetToken"]);

            string clientId = builder.Configuration["Paypal:Credentials:ClientId"] ?? throw new NullReferenceException("Client id not exists in configuration");
            string clientSecret = builder.Configuration["Paypal:Credentials:ClientSecret"] ?? throw new NullReferenceException("Client secret not exists in configuration");
            c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
        });
        builder.Services.AddHttpClient("CreateOrder", c => c.BaseAddress = new Uri(builder.Configuration["Paypal:Urls:CreateOrder"]));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}