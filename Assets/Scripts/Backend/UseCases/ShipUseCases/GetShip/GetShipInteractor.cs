using System;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.GetShip
{

    public class GetShipInteractor : IUseCaseInteractor<GetShipInputPort, IGetShipOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private IDataContext m_DataContext;
        private IObjectMapper m_Mapper;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GetShipInteractor(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetShipInputPort, IGetShipOutputPort>.Handle(
            GetShipInputPort inputPort, 
            IGetShipOutputPort outputPort)
        {
            Ship _Ship = this.m_DataContext.GetEntities<Ship>().SingleOrDefault(s => s.ID == inputPort.ID);
            
            if (_Ship == null)
                outputPort.PresentShipNotFound();
            else
                outputPort.PresentShip(this.m_Mapper.Map(_Ship, new ShipModel()));
        }

        #endregion Methods
  
    }

}