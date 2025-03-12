using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadableSlider : Slider
{

    #region - - - - - - Fields - - - - - -

    [SerializeField, RequiredField] private CanvasGroup m_LocalCanvasGroup;
    
    private bool m_IsFadingIn;
    private bool m_IsFadingOut;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    protected override void Start()
    {
        base.Start();
        this.m_LocalCanvasGroup = this.GetComponent<CanvasGroup>();
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void FadeIn()
    {
        if (Mathf.Approximately(this.m_LocalCanvasGroup.alpha, 1) || this.m_IsFadingIn) return;
        this.StartCoroutine(this.AnimateFade(FadeDirection.In, 0.2f));
        this.m_IsFadingIn = true;
    }

    public void FadeOut()
    {
        this.m_IsFadingIn = false;
        this.StartCoroutine(this.AnimateFade(FadeDirection.Out, 0.8f));
    }
    
    public IEnumerator AnimateFade(FadeDirection fadeDirection, float fadeTime)
    {
        float _CurrentFadeTime = 0;
        while (_CurrentFadeTime < fadeTime)
        {
            this.m_LocalCanvasGroup.alpha = fadeDirection == FadeDirection.In
                ? _CurrentFadeTime / fadeTime
                : fadeTime - _CurrentFadeTime / fadeTime;

            _CurrentFadeTime += Time.deltaTime;
            yield return null;
        }
    }

    #endregion Methods
  
}

public enum FadeDirection
{
    In,
    Out
}