using AutoMapper;
using ItemsDeTrabajo.Data;
using ItemsDeTrabajo.Dto;
using ItemsDeTrabajo.Models;
using ItemsDeTrabajo.Repositorio.Interfaz;
using ItemsDeTrabajo.Servicios.Interfaz;

namespace ItemsDeTrabajo.Servicios.Implementacion
{
    public class ItemTrabajoServicio : IItemTrabajoServicio
    {
        private readonly IItemTrabajoRepositorio _itemTrabajoRepositorio;
        private readonly IMapper _mapper;
        private readonly ItemTrabajoDBContext _itemTrabajoDBContext;


        public ItemTrabajoServicio(IItemTrabajoRepositorio itemTrabajoRepositorio, IMapper mapper, ItemTrabajoDBContext itemTrabajoDBContext)
        {
            _itemTrabajoRepositorio = itemTrabajoRepositorio;
            _mapper = mapper;
            _itemTrabajoDBContext = itemTrabajoDBContext;
        }

        public async Task<List<ItemTrabajo>> srvFindLstItemTrabajo()
        {
            return await _itemTrabajoRepositorio.srvFindLstItemTrabajo();
        }

        public async Task<int> srvSaveItemTrabajo(ItemTrabajoDto itemTrabajoDto)
        {
            int lintIdItem = itemTrabajoDto.IdItem.GetValueOrDefault();
            int result;
            if (lintIdItem == 0)
            {
                result = await _itemTrabajoRepositorio.srvInsertItemTrabajo(_mapper.Map<ItemTrabajo>(itemTrabajoDto));
            }
            else
            {
                result = await _itemTrabajoRepositorio.srvUpdateItemTrabajo(_mapper.Map<ItemTrabajo>(itemTrabajoDto));
            }

            return result;
        }

        public List<DistribucionItemTrabajo> srvDistribucionItemTrabajo(List<ItemTrabajoDto> lstItemTrabajo)
        {
            //Obtenemos la fecha actual
            DateTime ldtFechaActual = DateTime.Now;

            //Obtenemos listado de item de trabajo pendientes de asignacion ordernado por fecha y relevancia
            List<ItemTrabajo> pendientes = Datos.itemTrabajos
                .Where(x => x.AsignadoUsuario == 0)
                .OrderBy(x => x.FechaEntregaItem)
                .ThenBy(s => s.RelevanciaItem)
                .ToList();

            //Obtenemos el listado de usuarios que no tienen pendientes
            List<AuxiliarDto> lstUsuariosDisponibles = Datos.empleados
                .GroupJoin(Datos.distribucionItemTrabajos, e => e.IdEmpleado, id => id.IdEmpleado, (e, id) => new { e, id })
                .SelectMany(temp => temp.id.DefaultIfEmpty(), (tmp, id) => new { tmp.e, id })
                .Join(Datos.itemTrabajos, tmp => tmp.id.IdItem, item => item.IdItem, (tmp, item) => new { tmp.e, tmp.id, item })
                .Where(x => x.id.StatusItemTrabajo == 0) //consulto de acuerdo a los items pendientes del usuario
                .GroupBy(x => new { x.id.IdEmpleado, x.item.RelevanciaItem })
                .Select(x => new AuxiliarDto()
                {
                    IdEmpleado = x.Key.IdEmpleado,
                    NumItemsBaja = x.Where(s => s.item.RelevanciaItem == 1).Count(),
                    NumItemsAlta = x.Where(s => s.item.RelevanciaItem == 2).Count(),
                })
                .Where(s => s.NumItemsAlta <= 3) //tomo en consideracion para que no sean usuario saturados
                .OrderBy(s => s.NumItemsBaja)
                .ThenBy(s => s.NumItemsAlta)
                .ToList();

            //De acuerdo a los item de trabajo existentes validamos de acuerdo a las indicaciones para ser asignados
            foreach (var itemTrabajoDto in lstItemTrabajo.Where(x => x.AsignadoUsuario == 0).ToList())
            {
                // Crear una instancia de Random
                Random random = new();

                // Seleccionar un usuario al azar
                int indiceAleatorio = random.Next(lstUsuariosDisponibles.Count);
                AuxiliarDto usuarioAsignar = new();

                //Obtenemos la fecha de entrega del item de trabajo
                var ldtFechaEntrega = itemTrabajoDto.FechaEntregaItem;

                if (ldtFechaEntrega < ldtFechaActual.AddDays(3))
                {
                    usuarioAsignar = lstUsuariosDisponibles[indiceAleatorio];
                    DistribucionItemTrabajo distribucionItemTrabajo = new()
                    {
                        IdItem = itemTrabajoDto.IdItem.GetValueOrDefault(),
                        IdEmpleado = usuarioAsignar.IdEmpleado,
                        FechaAsignacion = DateTime.Now,
                        StatusItemTrabajo = 0
                    };
                    Datos.distribucionItemTrabajos.Add(distribucionItemTrabajo);
                    itemTrabajoDto.AsignadoUsuario = 1;
                }
                //Los ítems relevantes se asignarán primero y a aquellos usuarios que tienen una menor lista de pendientes.
                else if (itemTrabajoDto.RelevanciaItem == 1)
                {
                    usuarioAsignar = lstUsuariosDisponibles[0]; //sera el primero que tenga menos items pendientes
                    DistribucionItemTrabajo distribucionItemTrabajo = new()
                    {
                        IdItem = itemTrabajoDto.IdItem.GetValueOrDefault(),
                        IdEmpleado = usuarioAsignar.IdEmpleado,
                        FechaAsignacion = DateTime.Now,
                        StatusItemTrabajo = 0
                    };
                    Datos.distribucionItemTrabajos.Add(distribucionItemTrabajo);
                    itemTrabajoDto.AsignadoUsuario = 1;
                    if (usuarioAsignar.NumItemsAlta > 3)
                        lstUsuariosDisponibles.Remove(usuarioAsignar);
                }
                else
                {
                    usuarioAsignar = lstUsuariosDisponibles[indiceAleatorio];
                    DistribucionItemTrabajo distribucionItemTrabajo = new()
                    {
                        IdItem = itemTrabajoDto.IdItem.GetValueOrDefault(),
                        IdEmpleado = usuarioAsignar.IdEmpleado,
                        FechaAsignacion = DateTime.Now,
                        StatusItemTrabajo = 0
                    };
                    Datos.distribucionItemTrabajos.Add(distribucionItemTrabajo);
                    itemTrabajoDto.AsignadoUsuario = 1;
                }
            }

            //Ordenar la lista de pendientes por usuario después de cada asignación.
            var resultado = Datos.distribucionItemTrabajos
                .GroupBy(x => new { x.IdEmpleado, x.StatusItemTrabajo })
                .Select(x => new DistribucionItemTrabajo
                {
                    IdEmpleado = x.Key.IdEmpleado,
                    StatusItemTrabajo = x.Key.StatusItemTrabajo,
                })
                .OrderBy(x => x.StatusItemTrabajo)
                .ToList();

            return resultado;
        }
    }
}
