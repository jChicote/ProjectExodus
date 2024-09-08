using System;
using System.Collections;
using ProjectExodus.UserInterface.Common.ButtonHandlers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu.GameSaveSlot
{

    public class GameSaveSlotView : MonoBehaviour, IGameSaveSlotView
    {

        #region - - - - - - Fields - - - - - -
        
        private const int MAX_DISPLAYNAME_LENGTH = 10;

        [Space]
        public UnityEvent OnPlaySelection;

        [Header("Content Groups")]
        [SerializeField] private GameObject m_GameSlotContentGroup;
        
        [Header("Views")] 
        [SerializeField] private Image m_BackgroundImage;
        [SerializeField] private Slider m_BackgroundSlider;
        [SerializeField] private Image m_SlotProfileImage;
        [SerializeField] private Button m_SlotButton;
        [SerializeField] private ButtonHoldHandler m_SlotButtonHoldHandler;
        [SerializeField] private TMP_Text m_SlotTitle;
        [SerializeField] private TMP_Text m_SlotLastAccessedDate;
        [SerializeField] private TMP_Text m_SlotPercentage;

        [Header("View Parameters")]
        [SerializeField] private Color m_BackgroundColor;
        [SerializeField] private Color m_EmptyBackgroundColor;
        [SerializeField] private float m_AnimationTime = 6f;
        
        private Coroutine m_PlayedCoroutine;
        
        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Awake()
        {
            this.m_SlotButtonHoldHandler.OnHoldStart.AddListener(this.StartButtonHoldAnimation);
            this.m_SlotButtonHoldHandler.OnRelease.AddListener(this.ResetButtonHoldAnimation);
        }

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        void IGameSaveSlotView.BindToViewModel(IGameSaveSlotNotifyEvents viewModelNotify)
        {
            viewModelNotify.OnDisplayGameSaveSlot += this.DisplayGameSaveSlot;
            viewModelNotify.OnPropertyChangeEvent += this.OnPropertyChanged;
            
            this.m_SlotButton.onClick.AddListener(viewModelNotify.SlotSelectionCommand.Execute);
            this.OnPlaySelection.AddListener(viewModelNotify.PlayGameSaveCommand.Execute);
        }
            
        private void DisplayGameSaveSlot(bool isEmpty)
        {
            if (isEmpty)
            {
                // This will be ideal to invoke when the user clears a slot
                this.m_GameSlotContentGroup.SetActive(false);
                this.m_BackgroundImage.color = this.m_EmptyBackgroundColor;
                return;
            }
            
            this.m_GameSlotContentGroup.SetActive(true);
            this.m_BackgroundImage.color = this.m_BackgroundColor;
        }

        private void OnPropertyChanged(string updateType, object sender)
        {
            switch (updateType)
            {
                case "SlotPercentage":
                    this.m_SlotPercentage.text = $"{(float)sender}%";
                    break;
                case "SlotTitle":
                    var _GameSaveName = (string)sender;
                    this.m_SlotTitle.text = _GameSaveName.Length > MAX_DISPLAYNAME_LENGTH 
                        ? _GameSaveName.Substring(0, MAX_DISPLAYNAME_LENGTH) + "..." 
                        : _GameSaveName;
                    break;
                case "SlotLastAccessedDate":
                    DateTime _LastAccessedDate = ((DateTime)sender);
                    this.m_SlotLastAccessedDate.text =
                        $"{_LastAccessedDate.Day}/" +
                        $"{_LastAccessedDate.Month}/" +
                        $"{_LastAccessedDate.Year}";
                    break;
                case "SlotProfileImage":
                    this.m_SlotProfileImage.sprite = ((Sprite)sender);
                    break;
            }
        }

        private void StartButtonHoldAnimation()
        {
            if (!this.m_GameSlotContentGroup.activeInHierarchy) return;
                
            this.m_PlayedCoroutine = this.StartCoroutine(this.AnimateBackgroundSlider());
        }

        private IEnumerator AnimateBackgroundSlider()
        {
            float _CurrentTime = 0f;
            float _TargetValue = 1f;

            while (!Mathf.Approximately(this.m_BackgroundSlider.value, 1.0f))
            {
                _CurrentTime += Time.deltaTime;
                float _NewValue = Mathf.SmoothStep(
                                    this.m_BackgroundSlider.value, 
                                    _TargetValue,
                                    _CurrentTime / this.m_AnimationTime);

                this.m_BackgroundSlider.value = _NewValue;
                yield return null;
            }

            this.m_BackgroundSlider.value = _TargetValue;
            this.OnPlaySelection?.Invoke();
        }

        private void ResetButtonHoldAnimation()
        {
            if (this.m_PlayedCoroutine != null)
                this.StopCoroutine(this.m_PlayedCoroutine);
            
            this.m_BackgroundSlider.value = 0;
        }

        #endregion Methods
  
    }

}