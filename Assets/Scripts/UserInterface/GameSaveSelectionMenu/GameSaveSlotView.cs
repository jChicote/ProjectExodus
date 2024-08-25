using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu
{

    public class GameSaveSlotView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Header("Content Groups")]
        [SerializeField] GameObject m_GameSlotContentGroup;
        [SerializeField] GameObject m_EmptySlotContentGroup;
        
        [Header("Views")] 
        [SerializeField] private Image m_SlotProfileImage;
        [SerializeField] private Button m_SlotButton;
        [SerializeField] private TMP_Text m_SlotTitle;
        [SerializeField] private TMP_Text m_SlotLastAccessedDate;
        [SerializeField] private TMP_Text m_SlotPercentage;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public GameObject GameSlotContentGroup => this.m_GameSlotContentGroup;

        public GameObject EmptySlotContentGroup => this.m_EmptySlotContentGroup;

        public Image SlotProfileImage => this.m_SlotProfileImage;

        public Button SlotButton => this.m_SlotButton;

        public TMP_Text SlotTitle => this.m_SlotTitle;

        public TMP_Text SlotLastAccessedDate => this.m_SlotLastAccessedDate;

        public TMP_Text SlotPercentage => this.m_SlotPercentage;

        #endregion Properties

    }

}