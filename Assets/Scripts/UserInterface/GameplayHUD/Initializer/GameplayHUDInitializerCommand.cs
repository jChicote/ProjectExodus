using ProjectExodus.Common.Services;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD.Initializer
{

    public class GameplayHUDInitializerCommand : ICommand
    {

        #region - - - - - - Fields - - - - - -

        private readonly IGameplayHUDView m_View;
        private readonly IGameplayHUDController m_Controller;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameplayHUDInitializerCommand(GameObject gameplayHUD)
        {
            this.m_View = gameplayHUD.GetComponent<IGameplayHUDView>();
            this.m_Controller = gameplayHUD.GetComponent<IGameplayHUDController>();
        }

        #endregion Constructors
          
        #region - - - - - - Methods - - - - - -

        void ICommand.Execute()
        {
            IGameplayHUDMediator _Mediator = new GameplayHUDMediator();
            _ = new GameplayHUDViewModel(_Mediator, this.m_View);
            
            this.m_Controller.Initialize(_Mediator);
        }

        bool ICommand.CanExecute() 
            => true;

        #endregion Methods
  
    }

}