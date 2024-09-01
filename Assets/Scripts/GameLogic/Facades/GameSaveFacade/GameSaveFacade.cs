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

        void IGameSaveFacade.CreateGameSave(CreateGameSaveInputPort inputPort, ICreateGameSaveOutputPort outputPort)
        {
            // Temporary returned result
            outputPort.PresentCreatedGameSave(new GameSaveModel()
            {
                CompletionProgress = 99f,
                GameSaveName = "Success Create 99",
                GameSlotDisplayIndex = 1,
                ID = Guid.NewGuid(),
                LastAccessedDate = DateTime.Now,
                ProfileImage = new ProfileImageModel()
                {
                    ID = 1,
                    Image = default(Sprite)
                }
            });
            Debug.Log("[LOG]: Invoked CreateGameSaveSlot usecase");
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
                    ProfileImage = new ProfileImageModel()
                    {
                        ID = 1,
                        Image = default(Sprite)
                    }
                },
                new GameSaveModel()
                {
                    CompletionProgress = 60f,
                    GameSaveName = "Test Name B",
                    GameSlotDisplayIndex = 1,
                    ID = Guid.NewGuid(),
                    LastAccessedDate = DateTime.Now,
                    ProfileImage = new ProfileImageModel()
                    {
                        ID = 0,
                        Image = default(Sprite)
                    }
                }
            };
            
            outputPort.PresentGameSaves(_TempListOfSaves);
        }

        #endregion Get GameSave Methods

        void IGameSaveFacade.UpdateGameSave(UpdateGameSaveInputPort inputPort, IUpdateGameSaveOutputPort outputPort)
        {
            // Temporary returned result
            outputPort.PresentUpdatedGameSave(new GameSaveModel()
            {
                CompletionProgress = 88f,
                GameSaveName = "Success Update",
                GameSlotDisplayIndex = 1,
                ID = Guid.NewGuid(),
                LastAccessedDate = DateTime.Now,
                ProfileImage = new ProfileImageModel()
                {
                    ID = 3,
                    Image = default(Sprite)
                }
            });
            Debug.Log("[LOG]: Invoked UpdateGameSaveSlot usecase");
        }
  
    }

}