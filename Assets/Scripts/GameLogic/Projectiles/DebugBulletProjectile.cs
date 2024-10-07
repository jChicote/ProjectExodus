namespace ProjectExodus.GameLogic.Projectiles
{

    public class DebugBulletProjectile : BaseProjectile
    {

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Update()
        {
            if (this.m_IsPaused) return;
            
            this.Move();
            this.m_LifespanTimer.TickTimer();
        }

        #endregion Unity Lifecycle Methods

    }

}