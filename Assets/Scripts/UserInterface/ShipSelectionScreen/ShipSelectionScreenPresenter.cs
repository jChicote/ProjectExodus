using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.Management.Enumeration;
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
        private IUserInterfaceController m_UserInterfaceController;

        private int m_SelectedIndex;
        private List<ShipModel> m_AvailableShips;
        private List<ShipAssetObject> m_AllShips;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IShipSelectionScreenPresenter.Initialize(
            List<ShipModel> availableShips, 
            IShipAssetProvider shipAssetProvider)
        {
            this.m_View = this.GetComponent<ShipSelectionScreenView>();
            this.m_AvailableShips = availableShips;
            this.m_AllShips = shipAssetProvider.GetAllShips();
            
            this.BindMethodsToView();
            this.SelectShipInCollection(0);
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
            this.m_SelectedIndex = Math.Clamp(this.m_SelectedIndex--, 0, this.m_AvailableShips.Count);
            this.SelectShipInCollection(this.m_SelectedIndex);
        }

        private void RightButtonSelection()
        {
            this.m_SelectedIndex = Math.Clamp(this.m_SelectedIndex++, 0, this.m_AvailableShips.Count);
            this.SelectShipInCollection(this.m_SelectedIndex);
        }

        private void SelectShip()
        {
            
        }

        private void ReturnBackToMenu() 
            => this.m_UserInterfaceController.OpenScreen(UIScreenType.MainMenu);

        #endregion Screen Event Methods
  
        #region - - - - - - Methods - - - - - -

        void IScreenStateController.HideScreen()
            => this.m_View.HideScreen();

        void IScreenStateController.ShowScreen()
            => this.m_View.ShowScreen();

        private void SelectShipInCollection(int index)
        {
            ShipAssetObject _SelectedShip = this.m_AllShips[index];
            if (this.m_AvailableShips.Any(sm => sm.AssetID == _SelectedShip.ID))
            {
                this.m_View.PresentAvailableShip(new SelectedShipDto()
                {
                    Model = this.m_AvailableShips.Single(sm => sm.AssetID == _SelectedShip.ID),
                    ShipAsset = _SelectedShip
                });
            }
            else
                this.m_View.PresentUnAvailableShip(new SelectedShipDto { ShipAsset = _SelectedShip });
            
            this.UpdateScreenStateControls();
        }

        private void UpdateScreenStateControls()
        {
            this.m_View.LeftButton.enabled = this.m_SelectedIndex > 0;
            this.m_View.RightButton.enabled = this.m_SelectedIndex < this.m_AvailableShips.Count;
            this.m_View.SelectButton.enabled =
                this.m_AvailableShips.Any(sm => sm.AssetID == this.m_AllShips[this.m_SelectedIndex].ID);
        }

        #endregion Methods

    }

}