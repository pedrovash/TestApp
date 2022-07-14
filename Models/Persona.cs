namespace TestApp.Models
{
    public class Persona
    {
        public int Id { get; set; } 
        public string Rut { get; set; } 

        public string Nombre { get; set; } 
        public string Apellido { get; set; } 
        public string FechaNacimiento { get; set; } 
        public string Direccion { get; set; } 
        public string Cargo { get; set; } 
        public string Estado { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        //public int? HorarioInId { get; set; }
        //public HorarioIn? HorarioIn { get; set; }

        //public int? HorarioOutId { get; set; }
        //public HorarioOut? HorarioOut { get; set; }
    }
}
