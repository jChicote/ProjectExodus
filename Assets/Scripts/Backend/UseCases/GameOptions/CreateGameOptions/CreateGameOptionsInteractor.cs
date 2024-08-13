using ProjectExodus.Backend.Repositories;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameOptions.CreateGameOptions
{

    public class CreateGameOptionsInteractor : 
        IUseCaseInteractor<CreateGameOptionsInputPort, ICreateGameOptionsOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapper m_Mapper;
        private readonly IDataRepository<Entities.GameOptions> m_Repository;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateGameOptionsInteractor(IObjectMapper mapper, IDataRepository<Entities.GameOptions> repository)
        {
            this.m_Mapper = mapper;
            this.m_Repository = repository;
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<CreateGameOptionsInputPort, ICreateGameOptionsOutputPort>.Handle(
            CreateGameOptionsInputPort inputPort, 
            ICreateGameOptionsOutputPort outputPort)
        {
            Entities.GameOptions _GameOptions = new Entities.GameOptions();
            
            this.m_Mapper.Map(inputPort, _GameOptions);
            this.m_Repository.Create(_GameOptions);
            
            outputPort.PresentCreatedGameOptions(_GameOptions);
        }

        #endregion Methods
  
    }

}