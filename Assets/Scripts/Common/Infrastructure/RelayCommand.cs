using System;
using ProjectExodus.Common.Services;

namespace ProjectExodus.Common.Infrastructure
{

    /// <summary>
    /// Responsible for handling command-based interactions between different classes with seperate responsibilities.
    /// </summary>
    /// <remarks>
    /// Especially useful when needing to encapsulate logic or data-bindings between Views
    /// and ViewModels.
    /// </remarks>
    public class RelayCommand<T> : ICommand<T>
    {

        #region - - - - - - Fields - - - - - -

        private readonly Action<T> m_Execute;
        private readonly Func<T, bool> m_CanExecute;

        #endregion Fields


        #region - - - - - - Constructors - - - - - -

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.m_CanExecute = canExecute;
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        public void Execute(T parameter) => this.m_Execute(parameter);

        public bool CanExecute(T parameter) => this.m_CanExecute == null || this.m_CanExecute(parameter);

        #endregion Methods
  
    }

}