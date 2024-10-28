using Moq;
using NUnit.Framework;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace Tests.Backend.UseCases.GameSaveUseCases.CreateGameSaveUseCase
{

    public class CreateGameSaveInteractorTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly Mock<ICreateGameSaveOutputPort> m_MockOutputPort = new();
        private readonly Mock<IDataContext> m_MockDataContext = new();
        private readonly Mock<IObjectMapper> m_MockMapper = new();

        private IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort> m_Interactor;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateGameSaveInteractorTests() 
            => this.m_Interactor = new CreateGameSaveInteractor(this.m_MockDataContext.Object, this.m_MockMapper.Object);

        #endregion Constructors

        #region - - - - - - Handle Tests - - - - - -

        [Test]
        public void Handle_CreatingGameSave_GameSaveCreatedSuccessfully()
        {
            // Arrange
            
            // Act
            this.m_Interactor.Handle(new CreateGameSaveInputPort(), this.m_MockOutputPort.Object);
            
            // Assert
            this.m_MockDataContext.Verify(mock => mock.Add(It.IsAny<Player>()), Times.Once);
            this.m_MockDataContext.Verify(mock => mock.Add(It.IsAny<GameSave>()), Times.Once);
            this.m_MockOutputPort.Verify(mock => 
                mock.PresentCreatedGameSave(It.IsAny<GameSaveModel>()));
        }

        #endregion Handle Tests
  
    }

}