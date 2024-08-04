namespace ProjectExodus.Management.GameStateManager
{

    public interface IGameStateManager
    {

        #region - - - - - - Properties - - - - - -

        GameState GameState { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -
        
        void InitialiseGameStateManager();

        void ChangeGameState(GameState gameState);

        #endregion Methods

    }

}