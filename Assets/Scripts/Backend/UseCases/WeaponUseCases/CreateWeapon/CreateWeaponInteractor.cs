using System;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon
{

    public class CreateWeaponInteractor : IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;
        
        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateWeaponInteractor(IDataContext dataContext, IObjectMapper objectMapper)
        {
            this.m_DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort>.Handle(
            CreateWeaponInputPort inputPort, 
            ICreateWeaponOutputPort outputPort)
        {
            Weapon _Weapon = new Weapon();
            this.m_Mapper.Map(inputPort, _Weapon);
            this.m_DataContext.Add(_Weapon);

            WeaponModel _WeaponModel = new WeaponModel();
            this.m_Mapper.Map(_Weapon, _WeaponModel);
            outputPort.PresentCreatedWeapon(_WeaponModel);
        }

        #endregion Methods
  
    }

}