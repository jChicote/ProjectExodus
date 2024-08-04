namespace ProjectExodus.Management.GameStateManager
{

    public interface IGameStateManager
    {
  
        #region - - - - - - Methods - - - - - -
        
        void InitialiseGameStateManager();

        void ChangeGameState(GameState gameState);

        #endregion Methods

    }

}