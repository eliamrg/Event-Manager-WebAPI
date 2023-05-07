using Microsoft.AspNetCore.Authentication.JwtBearer;
using Event_manager_API.Filters;
using Event_manager_API.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Event_manager_API.Services;

namespace Event_manager_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Principio solid , nuestras clases deberian depender de abstracciones y no tipos concretos
            //var alumnosController = new AlumnosController(new ApplicationDbContext());
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opciones =>
            {
                opciones.Filters.Add(typeof(ExceptionFilter));
            }).AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Se encarga de configurar ApplicationDbContext como un servicio
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //Transient da nueva instancia de la clase declarada,
            //sirve para funciones que ejecutan una funcionalidad y listo, sin tener
            //que mantener información que será reutilizada en otros lugares
            services.AddTransient<IService, ServiceA>();
            //services.AddTransient<ServiceA>();
            services.AddTransient<ServiceTransient>();
            //Scoped el tiempo de vida de la clase declarada aumenta, sin embargo, Scoped da diferentes instancia
            //de acuerdo a cada quien mande la solicitud es decir Gustavo tiene su intancia y Alumno otra
            //services.AddScoped<IService, ServiceA>();
            services.AddScoped<ServiceScoped>();
            //Singleton se tiene la misma instancia siempre para todos los usuarios en todos los días,
            //todos los usuarios que hagan una petición van a tener la misma info compartida entre todos 
            //services.AddSingleton<IService, ServiceA>();
            services.AddSingleton<ServiceSingleton>();
            services.AddTransient<ActionFilter>();
            services.AddHostedService<WriteFile>();
            services.AddResponseCaching();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Event Manager Api",
                    Version = "",
                    Description = "An ASP.NET Core Web API for managing Events.\n\n Developed by Santiago Ramirez & Miguel Sanchez.",

                    Contact = new OpenApiContact
                    {
                        Name = "Contact",
                        Url = new Uri("https://eliamrg.github.io/PersonalWebSite/")
                    }
                }
                );
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //Use me permite agregar mi propio proceso sin afectar a los demas como Run
            //app.Use(async (context, siguiente) =>
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        //Se asigna el body del response en una variable y se le da el valor de memorystream
            //        var bodyOriginal = context.Response.Body;
            //        context.Response.Body = ms;

            //        //Permite continuar con la linea
            //        await siguiente.Invoke();

            //        //Guardamos lo que le respondemos al cliente en el string
            //        ms.Seek(0, SeekOrigin.Begin);
            //        string response = new StreamReader(ms).ReadToEnd();
            //        ms.Seek(0, SeekOrigin.Begin);

            //        //Leemos el stream y lo colocamos como estaba
            //        await ms.CopyToAsync(bodyOriginal);
            //        context.Response.Body = bodyOriginal;

            //        logger.LogInformation(response);
            //    }
            //});

            //Metodo para utilizar la clase middleware propia
            //app.UseMiddleware<ResponseHttpMiddleware>();

            //Metodo para utilizar la clase middleware sin exponer la clase. 
            app.UseResponseHttpMiddleware();


            //Atrapara todas las peticiones http que mandemos y retornar un string
            //Para detener todos los otros middleware se utiliza la funcion RUN

            //Para condicionar la ejecucion del middleware segun una ruta especifica se utiliza Map
            //Al utilizar Map permite que en lugar de ejecutar linealmente podemos agregar rutas especificas para
            // nuestro middleware
            app.Map("/maping", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Interceptando las peticiones");
                });
            });


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
