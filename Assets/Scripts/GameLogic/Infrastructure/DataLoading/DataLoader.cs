using System;
using System.Collections;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using UnityEngine;

namespace ProjectExodus.GameLogic.Infrastructure.DataLoading
{

    /// <summary>
    /// Responsible for running load-commands invoking back-end logic for resolving dependencies and data of
    /// services before they can be modified.
    /// </summary>
    public class DataLoader : IDataLoader
    {

        #region - - - - - - Methods - - - - - -

        public IEnumerator RunLoadOperation(ILoadCommand<object> command, Action<object> callback)
        {
            if (!command.CanExecute())
            {
                Debug.LogError($"[ERROR]: Load command '{command.GetLoadCommandName()}' cannot be executed.");
                yield return null;
            }
                
            command.Execute();

            bool _IsLoadComplete = false;
            while (!_IsLoadComplete)
            {
                _IsLoadComplete = command.IsLoadComplete();
                yield return null;
            }
            
            callback.Invoke(command.GetLoadedOptionsObject());
        }

        #endregion Methods
  
    }

}