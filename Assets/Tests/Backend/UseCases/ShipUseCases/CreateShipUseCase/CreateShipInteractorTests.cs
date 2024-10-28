using Moq;
using NUnit.Framework;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace Tests.Backend.UseCases.ShipUseCases.CreateShipUseCase
{

    public class CreateShipInteractorTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly Mock<ICreateShipOutputPort> m_MockOutputPort = new();
        private readonly Mock<IDataContext> m_MockDataContext = new();
        private readonly Mock<IObjectMapper> m_MockMapper = new();

        private IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort> m_Interactor;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateShipInteractorTests() 
            => this.m_Interactor = new CreateShipInteractor(this.m_MockDataContext.Object, this.m_MockMapper.Object);

        #endregion Constructors
  
        #region - - - - - - Handle Tests - - - - - -

        [Test]
        public void Handle_CreateNewShip_ShipIsCreatedSuccessfully()
        {
            // Arrange
            
            // Act
            this.m_Interactor.Handle(new CreateShipInputPort(), this.m_MockOutputPort.Object);
            
            // Assert
            this.m_MockDataContext.Verify(mock => mock.Add(It.IsAny<Ship>()), Times.Once);
            this.m_MockOutputPort.Verify(mock => mock.PresentCreatedShip(It.IsAny<ShipModel>()));
        }

        #endregion Handle Tests
  
    }

}