using System;
using System.Dynamic;
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

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateShipMapper(IDataContext dataContext, IObjectMapperRegister objectMapperRegister)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreateInputPortToShipEntity(CreateShipInputPort source, Ship destination)
        {
            
            
        }

        private void MapShipEntityToShipModel(Ship source, ShipModel destination)
        {
            
        }

        #endregion Methods
  
    }

}