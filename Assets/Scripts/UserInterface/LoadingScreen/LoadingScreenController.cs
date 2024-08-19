using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.LoadingScreen
{

    public class LoadingScreenController : MonoBehaviour, ILoadingScreenController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private Slider m_LoadingBarSlider;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        public void InitialiseLoadingScreenController()
        {
            throw new NotImplementedException();
        }

        #endregion Initializers
  
    }

}