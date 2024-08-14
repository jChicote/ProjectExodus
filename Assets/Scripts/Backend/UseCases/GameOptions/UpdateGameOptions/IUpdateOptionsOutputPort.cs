namespace ProjectExodus.Backend.UseCases.GameOptions.UpdateGameOptions
{

    public interface IUpdateOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentFailedUpdateOfGameOptions();
        
        void PresentSuccessfulUpdate(Entities.GameOptions gameOptions);

        #endregion Methods

    }

}