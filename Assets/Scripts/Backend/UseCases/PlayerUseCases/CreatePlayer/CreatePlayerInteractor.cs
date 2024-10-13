using System;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer
{

    public class CreatePlayerInteractor : IUseCaseInteractor<CreatePlayerInputPort, ICreatePlayerOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public CreatePlayerInteractor(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        public void Handle(CreatePlayerInputPort inputPort, ICreatePlayerOutputPort outputPort)
        {
            Player _Player = new Player();
            this.m_Mapper.Map(inputPort, _Player);
            this.m_DataContext.Add(_Player);

            PlayerModel _PlayerModel = new PlayerModel();
            this.m_Mapper.Map(inputPort, _Player);
            outputPort.PresentCreatedPlayer(_PlayerModel);
        }

        #endregion Methods
  
    }

}