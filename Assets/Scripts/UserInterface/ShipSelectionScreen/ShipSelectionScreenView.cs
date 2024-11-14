using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.ShipSelectionScreen
{

    public class ShipSelectionScreenView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;

        [Space] 
        [SerializeField] private TMP_Text m_ShipNameText;
        [SerializeField] private Image m_ShipImage;
        [SerializeField] private List<Image> m_WeaponTiles;
        [SerializeField] private Button m_LeftPageButton;
        [SerializeField] private Button m_RightPageButton;

        [Space] 
        [SerializeField] private Slider m_ShieldSlider;
        [SerializeField] private Slider m_PlatingSlider;
        [SerializeField] private Slider m_SpeedSlider;

        [Space] 
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

        public void HideScreen()
            => this.m_ContentGroup.SetActive(false);

        public void ShowScreen()
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

}