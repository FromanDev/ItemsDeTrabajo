namespace ItemsDeTrabajo.Models
{
    public class DistribucionItemTrabajo
    {
        public int IdDistribucion { get; set; }
        public int IdItem {  get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int StatusItemTrabajo { get; set; }
    }
}