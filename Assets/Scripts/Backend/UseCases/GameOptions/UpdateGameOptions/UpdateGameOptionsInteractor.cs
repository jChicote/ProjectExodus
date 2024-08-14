using ProjectExodus.Backend.Repositories;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameOptions.UpdateGameOptions
{

    public class UpdateGameOptionsInteractor : 
        IUseCaseInteractor<UpdateGameOptionsInputPort, IUpdateOptionsOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private IDataRepository<Entities.GameOptions> m_Repository;
        private IObjectMapper m_Mapper;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UpdateGameOptionsInteractor(
            IDataRepository<Entities.GameOptions> repository, 
            IObjectMapper mapper)
        {
            this.m_Repository = repository;
            this.m_Mapper = mapper;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<UpdateGameOptionsInputPort, IUpdateOptionsOutputPort>.Handle(
            UpdateGameOptionsInputPort inputPort, 
            IUpdateOptionsOutputPort outputPort)
        {
            
        }
        
        #endregion Methods

    }

}