using ItemsDeTrabajo.Dto;
using ItemsDeTrabajo.Models;

namespace ItemsDeTrabajo.Servicios.Interfaz
{
    public interface IItemTrabajoServicio
    {
        Task<List<ItemTrabajo>> srvFindLstItemTrabajo();
        Task<int> srvSaveItemTrabajo(ItemTrabajoDto itemTrabajoDto);
        List<DistribucionItemTrabajo> srvDistribucionItemTrabajo(List<ItemTrabajoDto> lstItemTrabajo);
    }
}