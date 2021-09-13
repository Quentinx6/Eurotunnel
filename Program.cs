using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API
{
    public class Program
    {
        // C'est le code qui est lu lorsque vous faite un dotnet run.
        // C'est le main program qui va tout distribuer ensuite.
        // C'est cours mais il y a BEAUCOUP de chose derrière ces simples lignes de code.
        // Pas d'inquiètude, pas besoin de comprendre non plus ici.
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try{
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migrations");
                }
            }

            host.Run();
        }

        // On précise ici ce qu'on veux utiliser comme fichier de reférence pour lancer notre serveur.
        // On utilise du coup STARTUP qui est en fait notre fichier Startup.cs
        // il reprends les fonctionnalités qu'on à ajouter et configurer à notre serveur.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
