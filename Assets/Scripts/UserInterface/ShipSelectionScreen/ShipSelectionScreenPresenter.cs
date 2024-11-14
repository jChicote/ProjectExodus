using System;
using System.Runtime.CompilerServices;
using ProjectExodus.Domain.Models;
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
        
        private ShipModel m_AvailableShips;
        private ShipAssetObject m_AllShips;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IShipSelectionScreenPresenter.Initialize()
        {
            this.m_View = this.GetComponent<ShipSelectionScreenView>();
            
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
            
        }

        private void RightButtonSelection()
        {
            
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
            
        }

        private void UpdateScreenStateControls()
        {
            
        }

        #endregion Methods

    }

}