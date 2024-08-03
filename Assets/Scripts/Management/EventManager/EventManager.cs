using UnityEngine;

namespace ProjectExodus.Management.EventManager
{

    /// <summary>
    /// Responsible for coordinating global game events.
    /// </summary>
    public class EventManager : MonoBehaviour, IEventManager
    {

        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            EventManager[] _EventManager = Object.FindObjectsByType<EventManager>(FindObjectsSortMode.None);
            if (_EventManager.Length > 1)
                Debug.LogError($"Multiple {nameof(EventManager)}s detected. " +
                               $"Only one {nameof(EventManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
    }

}