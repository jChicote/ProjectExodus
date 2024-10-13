using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer
{

    public class CreatePlayerMapper
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreatePlayerMapper(IDataContext dataContext, IObjectMapperRegister objectMapperRegister)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            
            objectMapperRegister
                .AddMappingAction<CreatePlayerInputPort, Player>(MapCreatePlayerInputPortToPlayerEntity);
            objectMapperRegister
                .AddMappingAction<Player, PlayerModel>(MapPlayerToPlayerModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreatePlayerInputPortToPlayerEntity(CreatePlayerInputPort source, Player destination)
        {
            destination.ID = Guid.NewGuid();
            destination.Ships = new() { source.StartShip.ID };
        }

        private void MapPlayerToPlayerModel(Player source, PlayerModel destination)
        {
            List<Ship> _PlayerShips = this.m_DataContext
                .GetEntities<Ship>()
                .Where(s => source.Ships.Any(id => id.Equals(s.ID)))
                .ToList();

            destination.ID = source.ID;
            destination.Ships = _PlayerShips;
        }
        
        #endregion Methods
  
    }

}