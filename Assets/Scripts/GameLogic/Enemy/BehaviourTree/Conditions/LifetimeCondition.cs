using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Has Lifetime Expired")]
    public class LifetimeCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        public FloatReference LifeTime = new(); 
        private float m_CurrentTimeAlive;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Update() 
            => this.m_CurrentTimeAlive += Time.deltaTime;

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        public override bool Check() 
            => this.m_CurrentTimeAlive > this.LifeTime.Value;

        #endregion Methods
  
    }

}