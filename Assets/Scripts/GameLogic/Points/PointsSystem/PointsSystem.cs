using System;
using Codice.CM.Common.Merge;
using ProjectExodus;
using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private IUIEventMediator m_UIEventMediator;
    private int m_TotalPoints;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        // TODO: Change to use interface
        EnemyObserver _Observer = EnemyManager.Instance.EnemyObserver;
        _Observer.OnEnemyDeath.AddListener(deathInfo =>
            this.AddPoints(deathInfo.Points, deathInfo.CanAddPoints, deathInfo.CanShowPoints, deathInfo.Position));

        this.m_UIEventMediator = UserInterfaceManager.Instance.EventMediator
            ?? throw new NullReferenceException( nameof(UserInterfaceManager.Instance.EventMediator));
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void AddPoints(int points, bool canAddPoints, bool canShowPoints, Vector2 presentationPosition)
    {
        if (!canAddPoints) return;
        
        if (canShowPoints)
        {
            this.m_UIEventMediator.Dispatch(PointsGUIConstants.AddPoints, new PointsInfo
            {
                Points = points,
                Position = presentationPosition
            });
        }
        
        this.UpdateTotalPoints(points);
    }

    private void UpdateTotalPoints(int points)
    {
        this.m_TotalPoints += points;
        this.m_UIEventMediator.Dispatch(PointsGUIConstants.UpdatePoints, this.m_TotalPoints);
    }

    #endregion Methods

}
