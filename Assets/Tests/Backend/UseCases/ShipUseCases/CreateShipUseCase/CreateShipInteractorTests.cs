using Moq;
using NUnit.Framework;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;

namespace Tests.Backend.UseCases.ShipUseCases.CreateShipUseCase
{

    public class CreateShipInteractorTests
    {

        #region - - - - - - Fields - - - - - -

        private Mock<IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort>> m_Interactor;

        #endregion Fields
  
        #region - - - - - - Handle Tests - - - - - -

        [Test]
        public void Handle_CreateNewShip_ShipIsCreated()
        {
            // Arrange
            
            // Act
            
            // Assert
            
        }

        #endregion Handle Tests
  
    }

}