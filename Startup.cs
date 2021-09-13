using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API
{
    public class Startup
    {
        // C'est le point d'entrer de l'application. Ca peut parraitre obscure ou magique, mais c'est pas le cas.
        // Pas besoin de vous posez des questions sur le sujet pour le moment.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Cette fonction sert à l'ajout de fonctionnalité d'un serveur dotnet.
        // Ici on va utiliser plusieurs fonctionnalité additionnel, le CORS, les Controllers,
        // L'intéraction avec la base de donnée.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddCors();
            services.AddDbContext<DataContext>(opt => 
            {
                opt.UseSqlite("Data Source=BaseDeDonnee.db");
            });
            services.AddScoped<IITAccountRepository, ITAccountRepository>();
            services.AddSwaggerGen();
        }

        // Cette fonction sert à la configuration des fonctionnalité d'un serveur dotnet.
        // Des fonctionnalité sont déjà natif et n'ont donc pas besoin d'être ajouté grace
        // a la fonction d'au dessus.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Petit bout de code pour ajouter des pages d'erreur lors du developpement.
            // Il est important de ne pas laisser de trace d'erreur trop précise en production
            // ces informations sont précieuses pour les pirates.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Utile pour faire de la redirection https automatique.
            // Un élément obligatoire aujourd'hui. Pour les besoin du dev, je le commente.

            //app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
            // Les routes sont les urls. On utilise logiquement cette fonctionnalité pour
            // un projet web.
            app.UseRouting();

            // Le CORS (se renseigner sur le net) est une première barrière à votre projet
            // C'est à dire que vous n'êtes pas obliger d'accepter tout le monde sur votre
            // application ( ici sur notre API ). Ce n'est pas Mc Do ... 
            // Pour les besoin du dev, j'ai accepter tout le monde, toute les méthodes et
            // tout les headers. C'est Open Bar
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // Utile pour gérer l'authentification et les autorisations de votre application.
            app.UseAuthorization();

            // Pour créer la "carte" des url disponible. Pas besoin de se prendre la tête, 
            // C'est gérer automatique ( c'est beau la technologie, non ?)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
