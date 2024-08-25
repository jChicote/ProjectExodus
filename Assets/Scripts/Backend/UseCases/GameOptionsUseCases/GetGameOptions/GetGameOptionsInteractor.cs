using System.Linq;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.UseCases.GameOptionsUseCases.GetGameOptions
{

    public class GetGameOptionsInteractor : 
        IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataRepository<GameOptions> m_Repository;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GetGameOptionsInteractor(IDataRepository<GameOptions> dataRepository) 
            => this.m_Repository = dataRepository;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetGameOptionsInputPort, IGetGameOptionsOutputPort>.Handle(
            GetGameOptionsInputPort inputPort, 
            IGetGameOptionsOutputPort outputPort)
        {
            var _GameOptions = this.m_Repository.GetEntities();
            outputPort.PresentGameOptions(_GameOptions);
        }

        #endregion Methods
  
    }

}
