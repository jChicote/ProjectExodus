using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.LoadingScreen
{

    public class LoadingScreenController : MonoBehaviour, ILoadingScreenController
    {

        #region - - - - - - Fields - - - - - -

        private const float SCALING_FACTOR = 100f;

        [SerializeField] private GameObject m_ContentGroup;
        [SerializeField] private Slider m_LoadingBarSlider;
        [SerializeField] private float m_LoadingLerpDuration = 2f;

        private float m_LoadProgress;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        public void InitialiseLoadingScreenController()
        {
            throw new NotImplementedException();
        }

        #endregion Initializers

        #region - - - - - - Methods - - - - - -
        void ILoadingScreenController.UpdateLoadProgress(float progress) 
            => this.StartCoroutine(AnimateLoadProgress(
                this.m_LoadProgress,
                this.m_LoadProgress + progress));

        void ILoadingScreenController.ResetLoadingScreen()
        {
            this.m_LoadProgress = 0f;
            this.m_LoadingBarSlider.value = this.m_LoadProgress;
        }

        void IScreenStateController.HideScreen()
            => this.m_ContentGroup.SetActive(false);

        void IScreenStateController.ShowScreen()
            => this.m_ContentGroup.SetActive(true);

        private IEnumerator AnimateLoadProgress(float startValue, float targetValue)
        {
            float _ElapsedDuration = 0f;
            while (_ElapsedDuration < m_LoadingLerpDuration)
            {
                this.m_LoadProgress =
                    Mathf.Lerp(startValue, targetValue, _ElapsedDuration / this.m_LoadingLerpDuration);
                this.m_LoadingBarSlider.value = this.m_LoadProgress / SCALING_FACTOR;
                _ElapsedDuration += Time.deltaTime;
                yield return null;
            }

            this.m_LoadProgress = targetValue;
            Mathf.Clamp(this.m_LoadProgress, 0f, 100f);
            this.m_LoadingBarSlider.value = this.m_LoadProgress / SCALING_FACTOR;
            
            yield return null;
        }

        #endregion Methods
    }

}