using ItemsDeTrabajo.Data;
using ItemsDeTrabajo.Models;
using ItemsDeTrabajo.Repositorio.Interfaz;
using Microsoft.EntityFrameworkCore;

namespace ItemsDeTrabajo.Repositorio.Implementacion
{
    public class ItemTrabajoRepositorio : IItemTrabajoRepositorio
    {
        private readonly ItemTrabajoDBContext _itemTrabajoDBContext;

        public ItemTrabajoRepositorio(ItemTrabajoDBContext itemTrabajoDBContext)
        {
            _itemTrabajoDBContext = itemTrabajoDBContext;
        }

        public async Task<List<ItemTrabajo>> srvFindLstItemTrabajo()
        {
            return await _itemTrabajoDBContext.ItemTrabajo.ToListAsync();
        }

        public async Task<ItemTrabajo> srvFindByRegiCodigo(int itemCodigo)
        {
            return await _itemTrabajoDBContext.ItemTrabajo.Where(x => x.IdItem == itemCodigo).FirstAsync();
        }

        public async Task<int> srvInsertItemTrabajo(ItemTrabajo itemTrabajo)
        {
            await _itemTrabajoDBContext.AddAsync(itemTrabajo);
            return itemTrabajo.IdItem;
        }

        public async Task<int> srvUpdateItemTrabajo(ItemTrabajo itemTrabajo)
        {
            var myLDato = await _itemTrabajoDBContext.ItemTrabajo.Where(x => x.IdItem == itemTrabajo.IdItem).FirstAsync();
            if (myLDato != null)
            {
                _itemTrabajoDBContext.ItemTrabajo.Attach(myLDato).CurrentValues.SetValues(itemTrabajo);
                _itemTrabajoDBContext.ChangeTracker.DetectChanges();
            }
            return itemTrabajo.IdItem;
        }

        public async Task srvDeleteItemTrabajo(int idItem)
        {
            ItemTrabajo empleadoHorario = await _itemTrabajoDBContext.ItemTrabajo.Where(x => x.IdItem == idItem).FirstAsync();

            if (empleadoHorario != null)
                _itemTrabajoDBContext.ItemTrabajo.Remove(empleadoHorario);
        }
    }
}
