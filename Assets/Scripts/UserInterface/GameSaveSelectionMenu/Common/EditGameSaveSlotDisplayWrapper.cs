using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using UserInterface.GameSaveSelectionMenu.Dtos;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.Common
{

    public class GameSaveSlotModelWrapper
    {

        #region - - - - - - Fields - - - - - -

        private GameSaveSlotDto m_GameSaveSlotDto;
        private ICreateGameSaveOutputPort m_CreateOutputPort;
        private IUpdateGameSaveOutputPort m_UpdateOutputPort;

        #endregion Fields
  
        #region - - - - - - Properties - - - - - -

        public GameSaveSlotDto GameSaveSlotDto => this.m_GameSaveSlotDto;

        public ICreateGameSaveOutputPort CreateOutputPort => this.m_CreateOutputPort;

        public IUpdateGameSaveOutputPort UpdateOutputPort => this.m_UpdateOutputPort;

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public GameSaveSlotModelWrapper(
            GameSaveSlotDto gameSaveSlotDto, 
            ICreateGameSaveOutputPort createOutputPort,
            IUpdateGameSaveOutputPort updateOutputPort)
        {
            this.m_GameSaveSlotDto = gameSaveSlotDto ?? throw new ArgumentNullException(nameof(gameSaveSlotDto));
            this.m_CreateOutputPort = createOutputPort ?? throw new ArgumentNullException(nameof(createOutputPort));
            this.m_UpdateOutputPort = updateOutputPort ?? throw new ArgumentNullException(nameof(updateOutputPort));
        }

        #endregion Constructors
  
    }

}