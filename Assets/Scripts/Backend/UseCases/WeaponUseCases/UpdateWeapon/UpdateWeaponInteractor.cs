using System;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon
{

    public class UpdateWeaponInteractor : IUseCaseInteractor<UpdateWeaponInputPort, IUpdateWeaponOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UpdateWeaponInteractor(IDataContext dataContext, IObjectMapper mapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<UpdateWeaponInputPort, IUpdateWeaponOutputPort>.Handle(
            UpdateWeaponInputPort inputPort,
            IUpdateWeaponOutputPort outputPort)
        {
            Weapon _Weapon = this.m_DataContext.GetEntities<Weapon>().Single(p => p.ID == inputPort.ID);
            this.m_Mapper.Map(inputPort, _Weapon);

            outputPort.PresentSuccessfulUpdate();
        }

        #endregion Methods
  
    }

}