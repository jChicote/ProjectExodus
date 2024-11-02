using System;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.ScriptableObjects.AssetEntities;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;

namespace ProjectExodus.UserInterface.GameplayHUD.Initializer
{

    public class GameplayHUDInitializerCommand : ICommand
    {

        #region - - - - - - Fields - - - - - -

        private readonly IGameplayHUDView m_View;
        private readonly IGameplayHUDMediator m_Mediator;
        private readonly ShipAssetObject m_ShipAsset;
        private readonly ShipModel m_SelectedShip;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameplayHUDInitializerCommand(IGameplayHUDView view, IGameplayHUDMediator mediator, ShipAssetObject shipAsset, ShipModel selectedShip)
        {
            this.m_View = view ?? throw new ArgumentNullException(nameof(view));
            this.m_Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.m_ShipAsset = shipAsset ?? throw new ArgumentNullException(nameof(shipAsset));
            this.m_SelectedShip = selectedShip ?? throw new ArgumentNullException(nameof(selectedShip));
        }

        #endregion Constructors
          
        #region - - - - - - Methods - - - - - -

        void ICommand.Execute()
        {
            // This has to be initialised somewhere else
            float _MaxPlatingHealth = this.m_ShipAsset.BasePlatingHealth + this.m_SelectedShip.PlatingHealthModifier;
            float _MaxShieldHealth = this.m_ShipAsset.BaseShieldHealth + this.m_SelectedShip.ShieldHealthModifier;
            this.m_View.Initialize(0, _MaxPlatingHealth, _MaxShieldHealth);
            
            _ = new GameplayHUDViewModel(this.m_Mediator, this.m_View);
        }

        bool ICommand.CanExecute() 
            => true;

        #endregion Methods
  
    }

}