using System;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer
{

    public class GetPlayerInteractor : IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetPlayerInteractor(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort>.Handle(
            GetPlayerInputPort inputPort, 
            IGetPlayerOutputPort outputPort)
        {
            Player _Player = this.m_DataContext
                .GetEntities<Player>()
                .FirstOrDefault(p => p.ID == inputPort.ID);
            
            if (_Player is null)
                outputPort.PresentPlayerNotFound();

            PlayerModel _PlayerModel = new PlayerModel();
            this.m_Mapper.Map(_Player, _PlayerModel);
            outputPort.PresentPlayer(_PlayerModel);
        }

        #endregion Methods
  
    }

}