using ProjectExodus.Domain.Entities;

namespace ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions
{

    public interface IUpdateOptionsOutputPort
    {

        #region - - - - - - Methods - - - - - -

        void PresentFailedUpdateOfGameOptions();
        
        void PresentSuccessfulUpdate(GameOptions gameOptions);

        #endregion Methods

    }

}