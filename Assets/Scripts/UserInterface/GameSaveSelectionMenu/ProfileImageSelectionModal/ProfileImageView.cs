using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.ProfileImageSelectionModal
{

    public class ProfileImageView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Header("Views")] 
        [SerializeField] private Image m_Image;
        [SerializeField] private Button m_Button;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Button Button => this.m_Button;

        public Image Image => this.m_Image;

        #endregion Properties

    }

}