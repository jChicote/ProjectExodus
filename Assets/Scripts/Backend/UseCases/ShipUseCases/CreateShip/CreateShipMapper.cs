using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip
{

    public class CreateShipMapper
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateShipMapper(IDataContext dataContext, IObjectMapper mapper, IObjectMapperRegister mapperRegister)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreateInputPortToShipEntity(CreateShipInputPort source, Ship destination)
        {
            
            
        }

        private void MapShipEntityToShipModel(Ship source, ShipModel destination)
        {
            List<WeaponModel> _Weapons = this.m_DataContext
                .GetEntities<Weapon>()
                .Select(w => this.m_Mapper.Map(w, new WeaponModel()))
                .ToList();
            
            destination.ID = source.ID;
            destination.Weapons = _Weapons;
        }

        #endregion Methods
  
    }

}