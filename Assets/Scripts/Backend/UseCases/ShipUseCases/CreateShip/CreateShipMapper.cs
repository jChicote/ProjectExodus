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
            
            mapperRegister.AddMappingAction<CreateShipInputPort, Ship>(this.MapCreateInputPortToShipEntity);
            mapperRegister.AddMappingAction<Ship, ShipModel>(this.MapShipEntityToShipModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreateInputPortToShipEntity(CreateShipInputPort source, Ship destination)
        {
            destination.ID = Guid.NewGuid();
            destination.AssetID = source.AssetID;
            destination.Weapons = source.Weapons;
        }

        private void MapShipEntityToShipModel(Ship source, ShipModel destination)
        {
            List<WeaponModel> _Weapons = this.m_DataContext
                .GetEntities<Weapon>()
                .Where(s => source.Weapons.Any(id => id == s.ID))
                .Select(w => this.m_Mapper.Map(w, new WeaponModel()))
                .ToList();
            
            destination.ID = source.ID;
            destination.AssetID = source.AssetID;
            destination.PlatingHealthModifier = source.PlatingHealthModifier;
            destination.ShieldHealthModifier = source.ShieldHealthModifier;
            destination.Weapons = _Weapons;
        }

        #endregion Methods
  
    }

}