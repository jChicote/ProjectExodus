using System.Linq;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.GameLogic.GameSettings;

namespace ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions
{

    public class GetGameOptionsInteractor : IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataRepository<Entities.GameOptions> m_GameOptionsRepository;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GetGameOptionsInteractor(IDataRepository<Entities.GameOptions> dataRepository) 
            => this.m_GameOptionsRepository = dataRepository;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>.Handle(
            GetGameOptionsInputPort inputPort, 
            IGetGameOptionsOutputPort outputPort)
        {
            var _GameOptions = this.m_GameOptionsRepository.GetEntities().FirstOrDefault();

            if (_GameOptions == null)
                _GameOptions = new Entities.GameOptions();
            
            outputPort.PresentGameOptions(_GameOptions);
        }

        #endregion Methods
  
    }

}
