using System;
using Domain.Contracts;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddDbContext<LibranzasContext>
			//   (opt => opt.UseInMemoryDatabase("Libranzas"));

			services.AddControllers().AddNewtonsoftJson(options =>
			options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
			services.AddDbContext<LibranzasContext>
			   (opt => opt.UseSqlServer(@"Server=LAPTOP-GEQ2K9D2\MSSQLSERVER01;Database=Libranzas;Trusted_Connection=True;MultipleActiveResultSets=true"));
			///Inyecci�n de dependencia Especifica
			//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0#register-additional-services-with-extension-methods
			services.AddScoped<IUnitOfWork, UnitOfWork>(); //Crear Instancia por peticion
			services.AddScoped<IDbContext, LibranzasContext>(); //Crear Instancia por peticion

			services.AddControllers();


			#region SwaggerOpen Api
			//Register the Swagger services
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Task API",
					Description = "Task API - ASP.NET Core Web API",
					TermsOfService = new Uri("https://cla.dotnetfoundation.org/"),
					Contact = new OpenApiContact
					{
						Name = "Unicesar",
						Email = string.Empty,
						Url = new Uri("https://github.com/borisgr04/CrudNgDotNetCore3"),
					},
					License = new OpenApiLicense
					{
						Name = "Licencia dotnet foundation",
						Url = new Uri("https://www.byasystems.co/license"),
					}
				});
			});

			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			#region Activar SwaggerUI
			app.UseSwagger();
			app.UseSwaggerUI(
				options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "Signus Prespuesto v1");
				}
			);
			#endregion

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
