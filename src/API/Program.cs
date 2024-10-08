using Business.Base;
using Business.Contracheque;
using Business.Contracheque.Interfaces;
using Business.Desconto.Services;
using Business.Users;
using Business.Users.Interfaces;
using Infra;
using Infra.Users.Repository;
using System.Text.Json.Serialization;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ExcecaoFiltro>();
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddDbContext<DatabaseContext>();
            builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
            builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            builder.Services.AddScoped<DescontoInssService>();
            builder.Services.AddScoped<DescontoIrpfService>();
            builder.Services.AddScoped<DescontoPlanosService>();
            builder.Services.AddScoped<IContrachequeService, ContrachequeService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "API.xml");
                options.IncludeXmlComments(filePath);
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
