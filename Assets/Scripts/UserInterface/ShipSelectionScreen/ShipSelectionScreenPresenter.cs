using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameDataManager;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.ScriptableObjects.AssetEntities;
using ProjectExodus.UserInterface.Controllers;
using UnityEngine;

namespace ProjectExodus.UserInterface.ShipSelectionScreen
{

    public class ShipSelectionScreenPresenter: 
        MonoBehaviour,
        IShipSelectionScreenPresenter,
        IScreenStateController
    {

        #region - - - - - - Fields - - - - - -
        
        private ShipSelectionScreenView m_View;
        private IGameStateManager m_GameStateManager;
        private IUserInterfaceController m_UserInterfaceController;
        private IWeaponAssetProvider m_WeaponAssetProvider;

        private int m_SelectedIndex;
        private Guid? m_SelectedShipID;
        private List<ShipModel> m_PlayerShips;
        private List<ShipAssetObject> m_AllShips;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IShipSelectionScreenPresenter.Initialize(
            IGameStateManager gameStateManager,
            IShipAssetProvider shipAssetProvider,
            IUserInterfaceController userInterfaceController,
            IWeaponAssetProvider weaponAssetProvider)
        {
            this.m_AllShips = shipAssetProvider.GetAllShips();
            this.m_GameStateManager = gameStateManager ?? throw new ArgumentNullException(nameof(gameStateManager));
            this.m_UserInterfaceController = userInterfaceController ??
                                             throw new ArgumentNullException(nameof(userInterfaceController));
            this.m_View = this.GetComponent<ShipSelectionScreenView>();
            this.m_WeaponAssetProvider =
                weaponAssetProvider ?? throw new ArgumentNullException(nameof(weaponAssetProvider));
            
            this.BindMethodsToView();
        }

        #endregion Initializers

        #region - - - - - - Screen Event Methods - - - - - -

        private void BindMethodsToView()
        {
            this.m_View.LeftButton.onClick.AddListener(this.LeftButtonSelection);
            this.m_View.RightButton.onClick.AddListener(this.RightButtonSelection);
            this.m_View.SelectButton.onClick.AddListener(this.SelectShip);
            this.m_View.BackButton.onClick.AddListener(this.ReturnBackToMenu);
        }

        private void LeftButtonSelection()
        {
            this.m_SelectedIndex = Math.Clamp(this.m_SelectedIndex - 1, 0, this.m_AllShips.Count - 1);
            this.SelectShipInCollection(this.m_SelectedIndex);
        }

        private void RightButtonSelection()
        {
            this.m_SelectedIndex = Math.Clamp(this.m_SelectedIndex + 1, 0, this.m_AllShips.Count - 1);
            this.SelectShipInCollection(this.m_SelectedIndex);
        }

        private void SelectShip()
        {
            ShipAssetObject _SelectedShip = this.m_AllShips[this.m_SelectedIndex];
            // SceneManager.Instance.SelectedShipID = this.m_PlayerShips.Single(sm => sm.AssetID == _SelectedShip.ID).ID; // Need to remove
            GameDataManager.Instance.SelectedShip = this.m_PlayerShips.Single(sm => sm.AssetID == _SelectedShip.ID);
            this.m_GameStateManager.ChangeGameState(GameState.Gameplay);
        }

        private void ReturnBackToMenu() 
            => this.m_UserInterfaceController.OpenScreen(UIScreenType.MainMenu);

        #endregion Screen Event Methods
  
        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen()
            => this.m_View.HideScreen();

        void IScreenStateController.ShowScreen()
        {
            this.m_PlayerShips = GameDataManager.Instance.PlayerShips;
            this.SelectShipInCollection(0);
            
            this.m_View.ShowScreen();
        }

        private void SelectShipInCollection(int index)
        {
            if (this.m_PlayerShips == null || this.m_PlayerShips.Count == 0)
                return;
            
            ShipAssetObject _SelectedShip = this.m_AllShips[index];
            if (this.m_PlayerShips.Any(sm => sm.AssetID == _SelectedShip.ID))
            {
                ShipModel _ShipModel = this.m_PlayerShips.Single(sm => sm.AssetID == _SelectedShip.ID);
                List<WeaponAssetObject> _WeaponAssets = _ShipModel.Weapons
                    .Select(wm => this.m_WeaponAssetProvider.Provide(wm.AssetID))
                    .ToList();
                
                this.m_View.PresentAvailableShip(new SelectedShipDto()
                {
                    Model = _ShipModel,
                    ShipAsset = _SelectedShip,
                    WeaponAssets = _WeaponAssets
                });
            }
            else
                this.m_View.PresentUnAvailableShip(new SelectedShipDto { ShipAsset = _SelectedShip });
            
            this.UpdateScreenStateControls();
        }

        private void UpdateScreenStateControls()
        {
            this.m_View.LeftButton.interactable = this.m_SelectedIndex > 0;
            this.m_View.RightButton.interactable = this.m_SelectedIndex < this.m_AllShips.Count - 1;
            this.m_View.SelectButton.interactable =
                this.m_PlayerShips.Any(sm => sm.AssetID == this.m_AllShips[this.m_SelectedIndex].ID);
        }

        #endregion Methods

    }

}