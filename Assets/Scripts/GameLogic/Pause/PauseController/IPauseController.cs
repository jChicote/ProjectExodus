namespace ProjectExodus.GameLogic.Pause.PauseController
{

    public interface IPauseController
    {

        #region - - - - - - Methods - - - - - -

        void PauseAllPausableComponents();
        
        void UnPauseAllPausableComponents();

        #endregion Methods

    }

}