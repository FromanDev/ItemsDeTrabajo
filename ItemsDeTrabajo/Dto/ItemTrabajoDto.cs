﻿namespace ItemsDeTrabajo.Dto
{
    public class ItemTrabajoDto
    {
        public int? IdItem { get; set; }
        public string? NombreItem { get; set; }
        public DateTime? FechaEntregaItem { get; set; }
        public int? RelevanciaItem { get; set; }
        public int AsignadoUsuario { get; set; }
    }
}