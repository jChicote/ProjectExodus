using MBT;
using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Utility.GameValidation;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPauseHandler : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    // Required Dependencies
    [SerializeField, RequiredField] private Blackboard m_Blackboard;
    private IPauseController m_PauseController;

    private BoolVariable m_IsPausedVariable;
    private UnityAction m_OnPause;
    private UnityAction m_OnUnpause;
    
    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_PauseController = SceneManager.Instance.SceneController.PauseController;
        this.m_IsPausedVariable = this.m_Blackboard.GetVariable<BoolVariable>(EnemyGeneralConstants.IsPaused);

        string _SourceObjectName = this.transform.root.gameObject.name;
        GameValidator.NotNull(this.m_Blackboard, nameof(m_Blackboard), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_IsPausedVariable, nameof(m_IsPausedVariable), sourceObjectName: _SourceObjectName);
        GameValidator.NotNull(this.m_PauseController, nameof(m_PauseController), sourceObjectName: _SourceObjectName);

        this.m_OnPause = () => this.m_IsPausedVariable.Value = true;
        this.m_OnUnpause = () => this.m_IsPausedVariable.Value = false;

        this.m_PauseController.SubscribePauseActionEvent(this.m_OnPause);
        this.m_PauseController.SubscribeUnPauseActionEvent(this.m_OnUnpause);
    }

    private void OnDestroy()
    {
        this.m_PauseController.UnSubscribePauseActionEvent(this.m_OnPause);
        this.m_PauseController.UnSubscribeUnPauseActionEvent(this.m_OnUnpause);
    }

    #endregion Unity Methods

}
