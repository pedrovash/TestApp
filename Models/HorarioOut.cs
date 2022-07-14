namespace TestApp.Models
{
    public class HorarioOut
    {
        public int Id { get; set; }
        public DateTime HorarioSalida { get; set; }

        public List<Persona>? Personas { get; set; }
    }
}
