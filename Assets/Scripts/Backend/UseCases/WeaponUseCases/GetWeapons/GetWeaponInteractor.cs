using System;
using System.Linq;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons
{

    public class GetWeaponInteractor : IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapper m_Mapper;
        private readonly IDataRepository<Weapon> m_Repository;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetWeaponInteractor(IObjectMapper mapper, IDataRepository<Weapon> dataRepository)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_Repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort>.Handle(
            GetWeaponInputPort inputPort, 
            IGetWeaponOutputPort outputPort)
        {
            var _Weapons = this.m_Repository
                .GetEntities()
                .Select(gs => this.m_Mapper.Map(gs, new WeaponModel()))
                .ToList();
            
            outputPort.PresentWeapons(_Weapons);
        }

        #endregion Methods
  
    }

}