using System.Linq;
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
            IObjectMapper mapper,
            IDataRepository<Entities.GameOptions> repository)
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
            var _GameOptions = this.m_Repository.GetEntities().FirstOrDefault(go => go.ID == inputPort.ID);
            if (_GameOptions == null)
            {
                outputPort.PresentFailedUpdateOfGameOptions();
                return;
            }
            
            this.m_Mapper.Map(inputPort, _GameOptions);
            this.m_Repository.Update(inputPort.ID, _GameOptions);
            
            outputPort.PresentSuccessfulUpdate(_GameOptions);
        }
        
        #endregion Methods

    }

}