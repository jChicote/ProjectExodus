using System.Linq;
using ProjectExodus.Backend.Entities;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.GameLogic.GameSettings;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions
{

    public class GetGameOptionsInteractor : IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataRepository m_DataRepository;
        private readonly GameSettings m_GameSettings;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GetGameOptionsInteractor(IDataRepository dataRepository, GameSettings gameSettings)
        {
            this.m_DataRepository = dataRepository;
            this.m_GameSettings = gameSettings;
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>.Handle(
            GetGameOptionsInputPort inputPort, 
            IGetGameOptionsOutputPort outputPort)
        {
            var _GameOptions = this.m_DataRepository.Get<GameLogic.Models.GameOptions>().First();
            this.m_GameSettings.SetGameOptions(_GameOptions);
        }

        #endregion Methods
  
    }

}