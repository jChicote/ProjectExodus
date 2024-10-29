using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDView : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Slider m_PlatingHealthBar;
        [SerializeField] private Slider m_ShieldHealthBar;
        [SerializeField] private Slider m_WeaponAmmoCountBar;
        
        [Space]
        [SerializeField] private Button m_PauseButton;

        #endregion Fields

    }

}