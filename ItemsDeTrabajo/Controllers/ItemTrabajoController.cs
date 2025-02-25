using ItemsDeTrabajo.Dto;
using ItemsDeTrabajo.Servicios.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace ItemsDeTrabajo.Controllers
{
    public class ItemTrabajoController : Controller
    {
        private readonly IItemTrabajoServicio _itemTrabajoServicio;

        public ItemTrabajoController(IItemTrabajoServicio itemTrabajoServicio)
        {
            _itemTrabajoServicio = itemTrabajoServicio;
        }

        [HttpGet("srvFindLstItemTrabajo")]
        public async Task<IActionResult> srvFindLstItemTrabajo()
        {
            return Ok(await _itemTrabajoServicio.srvFindLstItemTrabajo());
        }

        [HttpPost("srvSaveItemTrabajo")]
        public async Task<IActionResult> srvSaveItemTrabajo([FromBody] ItemTrabajoDto itemTrabajoDto)
        {
            return Ok(await _itemTrabajoServicio.srvSaveItemTrabajo(itemTrabajoDto));
        }

        [HttpPost("srvDistribucionItemTrabajo")]
        public IActionResult srvDistribucionItemTrabajo([FromBody] List<ItemTrabajoDto> lstItemTrabajoDto)
        {
            return Ok(_itemTrabajoServicio.srvDistribucionItemTrabajo(lstItemTrabajoDto));
        }
    }
}