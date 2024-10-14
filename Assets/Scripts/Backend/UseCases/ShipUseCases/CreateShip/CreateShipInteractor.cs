using System;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip
{

    public class CreateShipInteractor : IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public CreateShipInteractor(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort>.Handle(
            CreateShipInputPort inputPort, 
            ICreateShipOutputPort outputPort)
        {
            Ship _Ship = new Ship();
            this.m_Mapper.Map(inputPort, _Ship);
            this.m_DataContext.Add(_Ship);

            ShipModel _ShipModel = new ShipModel();
            this.m_Mapper.Map(_Ship, _ShipModel);
            outputPort.PresentCreatedShip(_ShipModel);
        }

        #endregion Methods
  
    }

}