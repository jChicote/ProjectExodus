using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.ScriptableObjects;

namespace ProjectExodus.Domain.Infrastructure.Providers
{

    public class ProfileImageModalProviderMapper
    {

        #region - - - - - - Constructors - - - - - -

        public ProfileImageModalProviderMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister
                .AddMappingAction<ProfileImageIDPair, ProfileImageModel>(
                    MapProfileImageIDPairToProfileImageModel);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapProfileImageIDPairToProfileImageModel(
            ProfileImageIDPair source,
            ProfileImageModel destination)
        {
            destination.ID = source.ID;
            destination.Image = source.Image;
        }

        #endregion Methods
  
    }

}