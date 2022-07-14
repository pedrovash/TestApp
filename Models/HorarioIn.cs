namespace TestApp.Models
{
    public class HorarioIn
    {
        public int Id{ get; set; }

        public DateTime HorarioEntrada { get; set; }

        public List<Persona>? Personas { get; set; }
    }
}
