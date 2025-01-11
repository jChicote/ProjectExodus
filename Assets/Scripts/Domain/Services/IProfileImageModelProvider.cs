using System.Collections.Generic;
using ProjectExodus.Domain.Models;

namespace ProjectExodus.Domain.Services
{

    public interface IProfileImageModelProvider
    {

        #region - - - - - - Methods - - - - - -

        ProfileImageModel Provide(int identifier);

        List<ProfileImageModel> ProvideAll();

        #endregion Methods

    }

}