namespace ProjectExodus.Backend.UseCases.GameOptions.GetGameOptions
{

    public interface IGetGameOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentGameOptions(GameLogic.Models.GameOptions gameOptions);

        #endregion Methods

    }

}