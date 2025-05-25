using System.Collections;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

public class FadableElement : PausableMonoBehavior
{
    [SerializeField, RequiredField] private CanvasGroup m_LocalCanvasGroup;
    [SerializeField] private AnimationCurve m_FadeAnimationCurve;
    [SerializeField] private float m_FadeInTime;
    [SerializeField] private float m_FadeOutTime;
    
    public void FadeIn()
    {
        if (Mathf.Approximately(this.m_LocalCanvasGroup.alpha, 1)) return;
        this.StartCoroutine(this.AnimateFade(FadeDirection.In, this.m_FadeInTime));
    }

    public void FadeOut()
    {
        this.StartCoroutine(this.AnimateFade(FadeDirection.Out, this.m_FadeOutTime));
    }
    
    private IEnumerator AnimateFade(FadeDirection fadeDirection, float fadeTime)
    {
        float _CurrentFadeTime = 0;
        while (_CurrentFadeTime < fadeTime)
        {
            float _Alpha = this.m_FadeAnimationCurve.Evaluate(_CurrentFadeTime / fadeTime);
            this.m_LocalCanvasGroup.alpha = fadeDirection == FadeDirection.In 
                ? _Alpha
                : 1 - _Alpha;

            _CurrentFadeTime += Time.deltaTime;
            yield return null;
        }
    }
}
