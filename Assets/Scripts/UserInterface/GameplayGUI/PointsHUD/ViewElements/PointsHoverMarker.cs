using ProjectExodus.GameLogic.Common.Timers;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using TMPro;
using UnityEngine;

public class PointsHoverMarker : PausableMonoBehavior
{

    #region - - - - - - Fields - - - - - -

    public TMP_Text m_PointsText;
    public float m_Lifetime;

    private EventTimer m_Timer;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start() 
        => this.m_Timer = new EventTimer(this.m_Lifetime, Time.deltaTime, () => Destroy(this.gameObject), canRun: true);

    private void Update()
    {
        if (this.m_IsPaused) return;
        
        this.m_Timer.TickTimer();
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void SetPoints(int points)
        => this.m_PointsText.SetText(points.ToString());
    
    #endregion Methods

}
