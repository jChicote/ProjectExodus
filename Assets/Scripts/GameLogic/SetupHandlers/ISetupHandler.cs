using GameLogic.SetupHandlers.SceneHandlers;

namespace GameLogic.SetupHandlers
{

    public interface ISetupHandler
    {

        #region - - - - - - Methods - - - - - -

        void SetNext(ISetupHandler next);

        void Handle(SceneSetupInitializationContext initializationContext);

        #endregion Methods

    }

}