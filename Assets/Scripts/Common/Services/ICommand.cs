namespace ProjectExodus.Common.Services
{

    public interface ICommand<in T>
    {

        #region - - - - - - Methods - - - - - -

        void Execute(T parameter);
        
        bool CanExecute(T parameter);

        #endregion Methods

    }
    
    public interface ICommand
    {

        #region - - - - - - Methods - - - - - -

        void Execute();

        bool CanExecute();

        #endregion Methods

    }

}