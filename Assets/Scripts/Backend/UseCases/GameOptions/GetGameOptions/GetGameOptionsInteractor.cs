using System.Linq;
using ProjectExodus.Backend.Repositories;

namespace ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions
{

    public class GetGameOptionsInteractor : IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataRepository<Entities.GameOptions> m_Repository;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GetGameOptionsInteractor(IDataRepository<Entities.GameOptions> dataRepository) 
            => this.m_Repository = dataRepository;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>.Handle(
            GetGameOptionsInputPort inputPort, 
            IGetGameOptionsOutputPort outputPort)
        {
            var _GameOptions = this.m_Repository.GetEntities().FirstOrDefault();

            // This is only default behavior to ensure that null is not being returned to the game scene.
            if (_GameOptions == null)
                _GameOptions = new Entities.GameOptions();
            
            outputPort.PresentGameOptions(_GameOptions);
        }

        #endregion Methods
  
    }

}
