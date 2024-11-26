using System;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerTargetingSystem
{

    public interface IPlayerTargetingSystem
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize(UnityEngine.Camera camera);

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void SearchForTarget(Vector2 screenPosition);

        #endregion Methods

    }
    
    public class PlayerTargetingSystem : PausableMonoBehavior, IPlayerTargetingSystem
    {

        #region - - - - - - Fields - - - - - -

        private UnityEngine.Camera m_Camera;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        void IPlayerTargetingSystem.Initialize(UnityEngine.Camera camera)
        {
            this.m_Camera = camera ?? throw new ArgumentNullException(nameof(camera));
        }

        #endregion Initializers

        #region - - - - - - Unity Methods - - - - - -

        private void Update()
        {
            
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        void IPlayerTargetingSystem.SearchForTarget(Vector2 screenPosition)
        {
            var _Value = this.m_Camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
            
            
        }

        private void RunTargetingSystem()
        {
            
        }

        private void RunTractorLockingSystem()
        {
            
        }

        #endregion Methods
  
    }

}