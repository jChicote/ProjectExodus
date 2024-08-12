using System.Linq;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.GameLogic.GameSettings;

namespace ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions
{

    public class GetGameOptionsInteractor : IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataRepository<Entities.GameOptions> m_DataRepository;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GetGameOptionsInteractor(IDataRepository<Entities.GameOptions> dataRepository) 
            => this.m_DataRepository = dataRepository;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>.Handle(
            GetGameOptionsInputPort inputPort, 
            IGetGameOptionsOutputPort outputPort)
        {
            var _GameOptions = this.m_DataRepository.Get().First();
            outputPort.PresentGameOptions(_GameOptions);
        }

        #endregion Methods
  
    }

}
