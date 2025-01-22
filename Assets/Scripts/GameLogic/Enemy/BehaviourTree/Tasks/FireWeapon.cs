using MBT;
using ProjectExodus.GameLogic.Common.Timers;
using UnityEngine;

namespace ProjectExodus
{

    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Fire Weapon")]
    public class FireWeapon : Leaf
    {
        public ActionReference WeaponHandlerReference = new();
        public WeaponFireReference WeaponFireReference = new();

        private EventTimer m_EventTimer;

        private void Start()
        {
            this.m_EventTimer = new(this.WeaponFireReference.Value.FireRate, Time.deltaTime, this.RunFireAction);
        }
        
        public override NodeResult Execute()
        {
            this.m_EventTimer.TickTimer();
            return NodeResult.success;
        }

        private void RunFireAction()
            => this.WeaponHandlerReference.Value?.Invoke();
    }

}