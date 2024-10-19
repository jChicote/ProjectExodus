using ProjectExodus.Common.Services;

namespace ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands
{

    public interface ILoadCommand<out TOptions>: ICommand
    {

        #region - - - - - - Methods - - - - - -

        string GetLoadCommandName();

        TOptions GetLoadedOptionsObject();

        bool IsLoadComplete();
        
        #endregion Methods

    }

}