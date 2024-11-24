using System;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip
{

    public class UpdateShipInteractor : IUseCaseInteractor<UpdateShipInputPort, IUpdateShipOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private IDataContext m_DataContext;
        private IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UpdateShipInteractor(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<UpdateShipInputPort, IUpdateShipOutputPort>.Handle(
            UpdateShipInputPort inputPort, 
            IUpdateShipOutputPort outputPort)
        {
            Ship _Ship = this.m_DataContext.GetEntities<Ship>().SingleOrDefault(s => s.ID == inputPort.ID);
            if (_Ship == null)
            {
                outputPort.PresentShipNotFound();
                return;
            }

            this.m_Mapper.Map(inputPort, _Ship);
            this.m_DataContext.Update(inputPort.ID, _Ship);
        }

        #endregion Methods
  
    }

}