using ItemsDeTrabajo.Models;

namespace ItemsDeTrabajo.Repositorio.Interfaz
{
    public interface IItemTrabajoRepositorio
    {
        Task<List<ItemTrabajo>> srvFindLstItemTrabajo();
        Task<ItemTrabajo> srvFindByRegiCodigo(int itemCodigo);
        Task<int> srvInsertItemTrabajo(ItemTrabajo itemTrabajo);
        Task<int> srvUpdateItemTrabajo(ItemTrabajo itemTrabajo);
        Task srvDeleteItemTrabajo(int idItem);
    }
}
