using System;

namespace API.Dtos
{
    public class ITAccountForRegisterDto
    {
        public string Matricule { get; set; }
        public string ITAccountID { get; set; }
        public string CurrentStep { get; set; }
    }
}