using MBT;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Thrust Forward Over Time")]
    public class ThrustForward : Leaf
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private TransformReference m_SourceTransform;
        [SerializeField] private float m_TraversalSpeed;
        
        // Might not be needed
        [SerializeField] private float m_TraversalTime; // TODO: Need to be calculated variable

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -
    
        public override NodeResult Execute()
        {
            if (this.m_SourceTransform.Value == null) return NodeResult.failure;

            this.m_SourceTransform.Value.position += this.m_SourceTransform.Value.up * this.m_TraversalSpeed * Time.deltaTime;
            Debug.Log("Is running");
            return NodeResult.success;
        }
        
        #endregion Methods
  
    }

}