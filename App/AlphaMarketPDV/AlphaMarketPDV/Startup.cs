using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using Microsoft.AspNetCore.Identity;

namespace AlphaMarketPDV
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<AlphaMarketPDVContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("AlphaMarketPDVContext"), builder =>
                    builder.MigrationsAssembly("AlphaMarketPDV")));

            services.AddScoped<ProdutoService>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<UnidadeMedidaService>();
            services.AddScoped<FormaPagamentoService>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<LojaService>();
            services.AddScoped<InfraService>();
            services.AddScoped<AdministrationService>();
            services.AddIdentity<UsuarioApp, PerfilApp>(
                options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<AlphaMarketPDVContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Infra/Acessar";
                options.AccessDeniedPath = "/Infra/AcessoNegado";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              AlphaMarketPDVContext context,
                              RoleManager<PerfilApp> roleManager,
                              UserManager<UsuarioApp> userManager)
        {
            var culturePadrao = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culturePadrao),
                SupportedCultures = new List<CultureInfo> {culturePadrao},
                SupportedUICultures = new List<CultureInfo> {culturePadrao}
            };

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DefaultDbInitializer.Initialize(context, userManager, roleManager).Wait();



        }
    }
}
