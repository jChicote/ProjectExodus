using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectExodus.UserInterface.Common.ButtonHandlers
{

    public class ButtonHoldHandler : 
        MonoBehaviour, 
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerDownHandler,  
        IPointerUpHandler
    {

        #region - - - - - - Fields - - - - - -

        [Space]
        public UnityEvent OnHoldStart;
        public UnityEvent OnRelease;

        [Space]
        [SerializeField] private float m_HoldTimerInterval = 0.5f;

        private float m_HoldTime;
        private bool m_IsHolding;
        private bool m_IsPressed;
        private bool m_IsWithinBounds;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Update()
        {
            bool _CanHold = this.m_IsWithinBounds && this.m_IsPressed;
            if (this.m_IsHolding || !_CanHold) return;

            this.m_HoldTime += Time.deltaTime;
            if (this.m_HoldTime >= this.m_HoldTimerInterval)
            {
                this.OnHoldStart?.Invoke();
                this.m_IsHolding = true;
            }
        }

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) 
            => this.m_IsWithinBounds = true;

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            this.m_IsWithinBounds = false;
            
            if (!this.m_IsHolding) return;
            this.ReleaseButton();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) 
            => this.m_IsPressed = true;

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            this.m_IsPressed = false;
            
            if (!this.m_IsHolding) return;
            this.ReleaseButton();
        }

        private void ReleaseButton()
        {
            this.OnRelease?.Invoke();
            this.m_IsHolding = false;
            this.m_HoldTime = 0;
        }

        #endregion Methods
  
    }

}