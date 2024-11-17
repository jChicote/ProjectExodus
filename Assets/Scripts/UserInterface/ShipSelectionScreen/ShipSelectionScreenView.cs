using System.Collections.Generic;
using ProjectExodus.Domain.Models;
using ProjectExodus.ScriptableObjects.AssetEntities;
using ProjectExodus.UserInterface.ShipSelectionScreen.WeaponTiles;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.ShipSelectionScreen
{

    public class ShipSelectionScreenView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;

        [FormerlySerializedAs("m_ShipNameText")]
        [Header("Ship Image Area")]
        [SerializeField] private TMP_Text m_ShipName;
        [SerializeField] private Image m_ShipImage;
        [SerializeField] private GameObject m_LockedShipWarning;
        
        [Header("Ship Weapon Area")]
        [SerializeField] private List<WeaponTileView> m_WeaponTiles;
        
        [Header("Ship Selection Buttons")]
        [SerializeField] private Button m_LeftPageButton;
        [SerializeField] private Button m_RightPageButton;

        [Header("Ship Stats Overview Area")]
        [SerializeField] private Slider m_ShieldSlider;
        [SerializeField] private Slider m_PlatingSlider;
        [SerializeField] private Slider m_SpeedSlider;

        [Header("Screen Submission Buttons")]
        [SerializeField] private Button m_SelectButton;
        [SerializeField] private Button m_BackButton;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button LeftButton
            => this.m_LeftPageButton;

        public Button RightButton
            => this.m_RightPageButton;

        public Button SelectButton
            => this.m_SelectButton;

        public Button BackButton
            => this.m_BackButton;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        public void PresentAvailableShip(SelectedShipDto selectedShip)
        {
            this.m_ShipName.text = selectedShip.ShipAsset.Name;
            this.m_ShipImage.sprite = selectedShip.ShipAsset.ShipSprite;

            if (selectedShip.WeaponAssets != null)
            {
                for (int _Index = 0; _Index < selectedShip.WeaponAssets.Count; _Index++)
                {
                    if (_Index > this.m_WeaponTiles.Count - 1)
                    {
                        Debug.LogWarning("[WARNING]: Number of weapons exceed the weapon tiles to present them.");
                        return;
                    }

                    WeaponTileView _WeaponTile = this.m_WeaponTiles[_Index];
                    _WeaponTile.WeaponSprite.sprite = selectedShip.WeaponAssets[_Index].WeaponSprite;
                    _WeaponTile.WeaponSprite.enabled = true;
                }
            }

            this.m_LockedShipWarning.SetActive(false);
        }

        public void PresentUnAvailableShip(SelectedShipDto selectedShip)
        {
            this.m_ShipName.text = selectedShip.ShipAsset.Name;
            this.m_ShipImage.sprite = selectedShip.ShipAsset.ShipSprite;

            foreach (WeaponTileView _WeaponTile in this.m_WeaponTiles)
                _WeaponTile.WeaponSprite.enabled = false;
            
            this.m_LockedShipWarning.SetActive(true);
        }

        public void HideScreen()
            => this.m_ContentGroup.SetActive(false);

        public void ShowScreen()
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

    public class SelectedShipDto
    {

        #region - - - - - - Properties - - - - - -

        public ShipModel Model { get; set; }
        
        public ShipAssetObject ShipAsset { get; set; }
        
        public List<WeaponAssetObject> WeaponAssets { get; set; }

        #endregion Properties
  
    }

}