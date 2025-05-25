using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Player.PlayerHealthSystem;
using UnityEngine;

namespace GameLogic.Pickups.Upgrades
{

    public class PlatingUpgradePickup : BasePickup
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private float m_UpgradeModifier;

        #endregion Fields
  
        #region - - - - - - Unity Methods - - - - - -

        private void Start() 
            => this.StartCoroutine(this.DestroyPickup());

        #endregion Unity Methods

        #region - - - - - - Unity Events - - - - - -

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != GameTag.Player) return;

            IPlayerHealthSystem _HealthSystem = other.GetComponent<IPlayerHealthSystem>();
            _HealthSystem.UpgradePlating(this.m_UpgradeModifier);
            
            Destroy(this.gameObject);
        }

        #endregion Unity Events
  
    }

}