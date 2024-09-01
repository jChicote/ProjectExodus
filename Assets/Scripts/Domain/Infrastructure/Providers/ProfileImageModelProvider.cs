using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.ScriptableObjects;

namespace ProjectExodus.Domain.Infrastructure.Providers
{

    public class ProfileImageModelProvider : IProfileImageModelProvider
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapper m_Mapper;
        private readonly UserInterfaceSettings m_UserInterfaceSettings;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ProfileImageModelProvider(IObjectMapper objectMapper, UserInterfaceSettings userInterfaceSettings)
        {
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_UserInterfaceSettings =
                userInterfaceSettings ?? throw new ArgumentNullException(nameof(userInterfaceSettings));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        ProfileImageModel IProfileImageModelProvider.Provide(int identifier)
        {
            if (this.m_UserInterfaceSettings.ProfileImages.All(pi => pi.ID != identifier))
                return new ProfileImageModel()
                {
                    ID = 0,
                    Image = default
                };

            var _ProfileImageModel = new ProfileImageModel();
            this.m_Mapper.Map(
                this.m_UserInterfaceSettings.ProfileImages.First(pi => pi.ID == identifier),
                _ProfileImageModel);
            
            return _ProfileImageModel;
        }

        List<ProfileImageModel> IProfileImageModelProvider.ProvideAll()
        {
            return this.m_UserInterfaceSettings.ProfileImages
                    .Select(pi => new ProfileImageModel()
                    {
                        ID = pi.ID,
                        Image = pi.Image
                    })
                    .ToList();
        }
        
        #endregion Methods

    }

}