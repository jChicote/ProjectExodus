using System;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer
{

    public class UpdatePlayerInteractor : 
        IUseCaseInteractor<UpdatePlayerInputPort, IUpdatePlayerOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private IDataContext m_DataContext;
        private IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UpdatePlayerInteractor(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<UpdatePlayerInputPort, IUpdatePlayerOutputPort>.Handle(
            UpdatePlayerInputPort inputPort, 
            IUpdatePlayerOutputPort outputPort)
        {
            Player _Player = this.m_DataContext
                .GetEntities<Player>()
                .Single(p => p.ID == inputPort.PlayerID);
            this.m_Mapper.Map(inputPort, _Player);

            outputPort.PresentSuccessfulUpdate();
        }

        #endregion Methods
  
    }

}