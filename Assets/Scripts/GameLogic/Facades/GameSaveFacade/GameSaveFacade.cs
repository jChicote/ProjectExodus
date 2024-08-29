using System;
using System.Collections.Generic;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Domain.Models;
using UnityEngine;

namespace ProjectExodus.GameLogic.Facades.GameSaveFacade
{

    public class GameSaveFacade : IGameSaveFacade
    {

        void IGameSaveFacade.CreateGameSave(ICreateGameSaveOutputPort outputPort)
        {
            throw new NotImplementedException();
        }

        #region - - - - - - Get GameSave Methods - - - - - -

        void IGameSaveFacade.GetGameSaves(IGetGameSavesOutputPort outputPort)
        {
            // TEMPORARY: Debug output for now
            var _TempListOfSaves = new List<GameSaveModel>()
            {
                new GameSaveModel()
                {
                    CompletionProgress = 20f,
                    GameSaveName = "Test Name A",
                    GameSlotDisplayIndex = 1,
                    ID = Guid.NewGuid(),
                    LastAccessedDate = DateTime.Now,
                    ProfileImage = default(Sprite)
                },
                new GameSaveModel()
                {
                    CompletionProgress = 60f,
                    GameSaveName = "Test Name B",
                    GameSlotDisplayIndex = 1,
                    ID = Guid.NewGuid(),
                    LastAccessedDate = DateTime.Now,
                    ProfileImage = default(Sprite)
                }
            };
            
            outputPort.PresentGameSaves(_TempListOfSaves);
        }

        #endregion Get GameSave Methods

        void IGameSaveFacade.UpdateGameSave(IUpdateGameSaveOutputPort outputPort)
        {
            throw new NotImplementedException();
        }
  
    }

}