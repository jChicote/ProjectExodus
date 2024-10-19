using System;
using System.Collections;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;

namespace ProjectExodus.GameLogic.Infrastructure.DataLoading
{

    public interface IDataLoader
    {

        #region - - - - - - Methods - - - - - -

        IEnumerator RunLoadOperation(ILoadCommand<object> command, Action<object> callback);

        #endregion Methods

    }

}