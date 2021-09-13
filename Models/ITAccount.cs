using System;

namespace API.Models
{
        public class ITAccount
    {
        public Guid ID {get; set;}
        public String Matricule {get; set;}
        public String ADAccountID {get; set;}
        public String CurrentStep {get; set;}
    }
}