using ItemsDeTrabajo.Models;
namespace ItemsDeTrabajo.Data
{
    public static class Datos
    {
        public static List<Empleado> empleados = new()
        {
            new(){IdEmpleado=1,NombreEmpleado="Empleado 1"},
            new(){IdEmpleado=2,NombreEmpleado="Empleado 2"},
            new(){IdEmpleado=3,NombreEmpleado="Empleado 3"},
            new(){IdEmpleado=4,NombreEmpleado="Empleado 4"},
        };

        public static List<ItemTrabajo> itemTrabajos = new()
        {
            new(){IdItem=1, NombreItem="Item 1", FechaEntregaItem=new DateTime(2025,2,28), AsignadoUsuario=0, RelevanciaItem=1},
            new(){IdItem=2, NombreItem="Item 2", FechaEntregaItem=new DateTime(2025,2,27), AsignadoUsuario=1, RelevanciaItem=0},
            new(){IdItem=3, NombreItem="Item 3", FechaEntregaItem=new DateTime(2025,2,28), AsignadoUsuario=0, RelevanciaItem=1},
            new(){IdItem=4, NombreItem="Item 4", FechaEntregaItem=new DateTime(2025,2,27), AsignadoUsuario=1, RelevanciaItem=0},
            new(){IdItem=5, NombreItem="Item 5", FechaEntregaItem=new DateTime(2025,2,27), AsignadoUsuario=0, RelevanciaItem=1},
            new(){IdItem=6, NombreItem="Item 6", FechaEntregaItem=new DateTime(2025,2,28), AsignadoUsuario=0, RelevanciaItem=1}
        };

        public static List<DistribucionItemTrabajo> distribucionItemTrabajos = new()
        {
            new() { IdDistribucion = 1, FechaAsignacion = new DateTime(2025, 2, 24), IdEmpleado = 1, IdItem = 2, StatusItemTrabajo = 0},
            new() { IdDistribucion = 2, FechaAsignacion = new DateTime(2025, 2, 24), IdEmpleado = 1, IdItem = 4, StatusItemTrabajo = 1},
        };
    }
}