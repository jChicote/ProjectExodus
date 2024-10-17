using ProjectExodus.Common.Services;

namespace ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands
{

    public interface ILoadCommand : ICommand
    {

        #region - - - - - - Methods - - - - - -

        string GetLoadCommandName();

        bool IsLoadComplete();

        #endregion Methods

    }

}