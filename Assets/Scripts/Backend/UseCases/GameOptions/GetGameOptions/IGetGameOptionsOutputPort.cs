namespace ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions
{

    public interface IGetGameOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentGameOptions(Entities.GameOptions gameOptions);

        #endregion Methods

    }

}