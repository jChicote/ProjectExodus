using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Has Exceeded Max Collisions")]
    public class CollisionHitDeathCondition : Condition
    {

        #region - - - - - - Fields - - - - - -

        public IntReference MaxCollisionHitCount;
        public IntReference CollisionHitCount;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override bool Check() 
            => this.CollisionHitCount.Value > this.MaxCollisionHitCount.Value;

        #endregion Methods
  
    }

}