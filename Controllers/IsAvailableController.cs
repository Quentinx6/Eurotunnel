using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    // Je précise à .NET que ce fichier est un Controller de l'application
    // Le controler est un peu le chef d'orchestre, il va demander des informations
    // et les restituer.
    [ApiController]
    // Je précise à .Net la route qu'il faut suivre pour atteindre ce controller.
    // ici ça serai donc http://localhost:5000/api/v1/IsAvailable
    // Le [] va prendre par defaut le nom du fichier sans le mot Controller.
    [Route("/api/v1/[controller]")]

    // Langage orienté objet oblige, le controller est un objet ( public ici )
    // Il est issus d'un autre objet natif à .NET le ControllerBase
    public class IsAvailableController : ControllerBase
    {
        // Je lui précise la nature de la méthode pour la route.
        // Il en existe beaucoup, ici ça sera un GET classique.
        [HttpGet]
        // Je détermine ensuite la fonction qui va être executer lorsque
        // je vais faire une requête en GET sur l'addresse du controller : 
        // http://localhost:5000/api/v1/IsAvailable
        public String CheckAvailability()
        {
            String Message = "L'API est bien en marche";
            return Message;
        }
    }
}