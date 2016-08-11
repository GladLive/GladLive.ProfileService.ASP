using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using GladNet.Serializer.Protobuf;

namespace GladLive.ProfileService.ASP
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddLogging();

			//This adds the MVC core features and GladNet features
			services.AddGladNet(new ProtobufnetSerializerStrategy(), new ProtobufnetDeserializerStrategy(), new ProtobufnetRegistry());

			//Register DB services
			services.AddEntityFrameworkSqlServer()
				.AddDbContext<ProfileDbContext>(option =>
				{
					option.UseInMemoryDatabase(); //for test and dev we should use in-memory

					//option.UseSqlServer(@"Server=Glader-PC;Database=ASPTEST;Trusted_Connection=True;");
					//option.UseMemoryCache(new MemoryCache(new MemoryCacheOptions()));
				});

			

			services.AddScoped<IProfileRepositoryAsync, ProfileRepository>();
			services.AddTransient<ProfileDbContext>();
		}

		public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			//Comment this out for IIS but we shouldn't need it. We'll be running this on
			//Linux behind AWS Elastic Load Balancer probably
			//app.UseIISPlatformHandler();

			loggerFactory.AddConsole(LogLevel.Information);

			JwtBearerOptions bearerOptions = new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				RequireHttpsMetadata = false,
				TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
				{
					IssuerSigningKey = new X509SecurityKey(new X509Certificate2(@"Certs/TestCert.pfx")),
					ValidateIssuerSigningKey = false, //WARNING: This is bad. We should validate the signing key in the future
					ValidateAudience = false,
					ValidateIssuer = true,
					ValidIssuer = "https://localhost:44300/",
					ValidateLifetime = false //temporary until we come up with a solution
				},
			};

			// use jwt bearer authentication
			app.UseJwtBearerAuthentication(bearerOptions);

			app.UseMvc();

			//We have to register the payload types
			//We could maybe do some static analysis to find referenced payloads and auto generate this code
			//or find them at runtime but for now this is ok
			new ProtobufnetRegistry().RegisterCommonGladLivePayload();
		}

		//This changed in RTM. Fluently build and setup the web hosting
		public static void Main(string[] args) => new WebHostBuilder()
			.UseKestrel(options =>
			{
				X509Certificate2 cert = new X509Certificate2(@"Certs/certificate.pfx");

				//TODO: Handle cert changes
				options.UseHttps(new HttpsConnectionFilterOptions() { SslProtocols = System.Security.Authentication.SslProtocols.Tls | System.Security.Authentication.SslProtocols.Tls11 | System.Security.Authentication.SslProtocols.Tls12, ServerCertificate = cert });
			})
			.UseUrls(@"https://localhost:44300")
			.UseContentRoot(Directory.GetCurrentDirectory())
			.UseStartup<Startup>()
			.Build()
			.Run();
	}
}