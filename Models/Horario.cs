namespace TestApp.Models
{
    public class Horario
    {
        public Guid Id { get; set; }

        public int PersonaId { get; set; }

        public Persona Persona { get; set; }

        public DateTime HoraMarcacion { get; set; }

        public string Tipo { get; set; }
        





    }
}
