using UnityEngine;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public class OptionsMenuController : MonoBehaviour, IOptionsMenuController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_ContentGroup;

        [Header("Audio Control Options")]
        [SerializeField] private GameObject m_AudioControlContentGroup;
        // [SerializeField] private 

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void IOptionsMenuController.InitialiseOptionsMenu()
        {
            
        }
        
        void IScreenStateController.HideScreen() 
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen() 
            => this.m_ContentGroup.SetActive(true);

        #endregion Methods

    }

}