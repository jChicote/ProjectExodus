namespace ProjectExodus.Backend.UseCases
{

    public interface IUseCaseInteractor<in TInputPort, in TOutputPort>
    {

        #region - - - - - - Methods - - - - - -

        void Handle(TInputPort inputPort, TOutputPort outputPort);

        #endregion Methods

    }

}