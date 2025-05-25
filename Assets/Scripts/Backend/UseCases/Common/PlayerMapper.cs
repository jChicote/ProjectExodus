using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.Common
{

    public class PlayerMapper
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PlayerMapper(IDataContext dataContext, IObjectMapper mapper, IObjectMapperRegister objectMapperRegister)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            
            objectMapperRegister
                .AddMappingAction<Player, PlayerModel>(this.MapPlayerToPlayerModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapPlayerToPlayerModel(Player source, PlayerModel destination)
        {
            List<ShipModel> _PlayerShips = this.m_DataContext
                .GetEntities<Ship>()
                .Where(s => source.Ships.Any(id => id == s.ID))
                .Select(s => this.m_Mapper.Map(s, new ShipModel()))
                .ToList();

            destination.ID = source.ID;
            destination.Ships = _PlayerShips;
        }

        #endregion Methods
  
    }

}