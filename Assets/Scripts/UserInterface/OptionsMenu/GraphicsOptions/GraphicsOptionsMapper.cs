using ProjectExodus.Backend.UseCases.GameOptions.UpdateGameOptions;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;

namespace ProjectExodus.UserInterface.OptionsMenu.GraphicsOptions
{

    public class GraphicsOptionsMapper
    {

        #region - - - - - - Constructors - - - - - -

        public GraphicsOptionsMapper(IObjectMapperRegister objectMapperRegister)
        {
            objectMapperRegister
                .AddMappingAction<GraphicsOptionViewModel, UpdateGameOptionsInputPort>(
                    MapGraphicsOptionViewModelToInputPort);
            objectMapperRegister
                .AddMappingAction<GameOptionsModel, GraphicsOptionViewModel>(
                    MapGameOptionsToGraphicsOptionViewModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapGraphicsOptionViewModelToInputPort(
            GraphicsOptionViewModel source,
            UpdateGameOptionsInputPort destination)
        {
            destination.DisplayHeight = source.DisplayHeight;
            destination.DisplayOption = source.DisplayOption;
            destination.DisplayWidth = source.DisplayWidth;
        }

        private static void MapGameOptionsToGraphicsOptionViewModel(
            GameOptionsModel source,
            GraphicsOptionViewModel destination)
        {
            destination.DisplayHeight = source.DisplayHeight;
            destination.DisplayOption = source.DisplayOption;
            destination.DisplayWidth = source.DisplayWidth;
        }

        #endregion Methods
  
    }

}