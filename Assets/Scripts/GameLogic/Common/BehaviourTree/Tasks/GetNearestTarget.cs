using MBT;
using UnityEngine;

public class GetNearestTarget : Leaf
{
    [SerializeField] private TransformReference m_TargetTransform = new();
    [SerializeField] private FloatReference m_DetectionDistance = new();
    
    public override NodeResult Execute()
    {
        throw new System.NotImplementedException();
    }
}
