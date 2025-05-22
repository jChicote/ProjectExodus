using System.Linq;
using MBT;
using ProjectExodus;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;
using TransformReference = ProjectExodus.TransformReference;

[AddComponentMenu("")]
[MBTNode(name = "Tasks/Get Nearest Target")]
public class GetNearestTarget : Leaf
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private FloatReference m_DetectionDistance = new();
    [SerializeField] private TransformReference m_TargetTransform = new();
    [SerializeField] private TransformReference m_SourceTransform = new();

    private EnemyCollection m_EnemyTrackedCollection;

    #endregion Fields
  
    #region - - - - - - Properties - - - - - -

    private float SqrDetectionDistance 
        => this.m_DetectionDistance.Value * this.m_DetectionDistance.Value;

    #endregion Properties
  
    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_EnemyTrackedCollection = EnemyManager.Instance.EnemyCollection;

        GameValidator.NotNull(
            this.m_EnemyTrackedCollection, 
            nameof(m_EnemyTrackedCollection),
            sourceObjectName: this.transform.root.gameObject.name);
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public override NodeResult Execute()
    {
        float _TrackedTargetDistance = (this.m_TargetTransform.Value.position - this.m_SourceTransform.Value.position)
            .sqrMagnitude;

        if (this.m_TargetTransform.Value == null)
            this.m_TargetTransform.Value = this.GetNearbyTarget();
        
        else if (_TrackedTargetDistance > this.SqrDetectionDistance)
            this.m_TargetTransform.Value = this.GetNearbyTarget();
        
        return NodeResult.success;
    }

    private Transform GetNearbyTarget()
    {
        Transform _NearestTarget = default;
        float _NearestEncounteredDistance = this.SqrDetectionDistance;
        
        for (int i = 0; i < this.m_EnemyTrackedCollection.Enemies.Count(); i++)
        {
            Transform _TargetTransform = this.m_EnemyTrackedCollection.Enemies[i].transform;
            float _Distance = (this.m_SourceTransform.Value.position - _TargetTransform.position).sqrMagnitude;

            if (_Distance < _NearestEncounteredDistance)
                _NearestTarget = _TargetTransform;
        }

        return _NearestTarget;
    }

    #endregion Methods
  
}
