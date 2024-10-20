using ProjectExodus.GameLogic.Facades.PlayerControllers;

namespace ProjectExodus.DebugSupport.Provider
{

    /// <summary>
    /// A hard-coded provider. Responsible for providing and ensuring there is a default GameSave model with all
    /// developer access to game assets.
    /// </summary>
    public class DebugDefaultGameSaveGenerator
    {
        
        public DebugDefaultGameSaveGenerator(
            IPlayerControllers playerControllers)
        {
            
        }
        
    }

}