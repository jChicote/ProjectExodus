using MBT;
using ProjectExodus;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Utility.BehaviourTree;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;
using TransformReference = MBT.TransformReference;
using TransformVariable = MBT.TransformVariable;

public class ZetoSentinelPortalBehaviourTreeConfiguration : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField, RequiredField] private Blackboard m_Blackboard;
    [SerializeField, RequiredField] private EnemySpawnDirector m_EnemySpawnDirector;
    private IPlayerObserver m_PlayerObserver;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_PlayerObserver = SceneManager.Instance.PlayerObserver;
        
        string _SourceObjectName = this.transform.gameObject.name;
        GameValidator.NotNull(this.m_Blackboard, nameof(m_Blackboard), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_EnemySpawnDirector, nameof(m_EnemySpawnDirector), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_PlayerObserver, nameof(m_PlayerObserver), sourceObjectName: _SourceObjectName);
        
        this.Configure();
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    private void Configure()
    {
        IPlayerProvider _PlayerProvider = SceneManager.Instance.SceneController.PlayerProvider;        
        
        // Get Variable References
        Debug.Log(this.m_Blackboard.GetVariable<TransformVariable>(EnemyGeneralConstants.TargetTransform).GetType());
        TransformVariable _TargetTransformVariable =
            this.m_Blackboard.GetVariable<TransformVariable>(EnemyGeneralConstants.TargetTransform);
        
        // Validate References
        string _SourceObjectName = this.transform.gameObject.name;
        MBTUtils.Validate(_TargetTransformVariable, sourceObjectName: _SourceObjectName);

        _TargetTransformVariable.Value = _PlayerProvider.GetActivePlayer().transform;
        this.m_PlayerObserver.OnPlayerSpawned.AddListener(newPlayer => _TargetTransformVariable.Value = newPlayer.transform);
    }

    #endregion Methods
  
}
