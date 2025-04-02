using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatboxHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_ContentGroup;

    [Header("View Elements")]
    [SerializeField] private Image m_Background;
    
    [Header("ChatBox Content")]
    [SerializeField] private TMP_Text m_ChatboxText;
    [SerializeField] private RectTransform m_ChatboxRect;
    [SerializeField] private float m_TypingSpeed = 0.05f;
    [SerializeField] private float m_PostTextLifetime;
    
    [Header("Scoll Fields")]
    [SerializeField] private ScrollRect m_ChatboxScroll;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start() 
        => this.m_ChatboxText.text = "";

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void SetChatText(string text)
    {
        this.m_ChatboxText.text = "";
        this.StartCoroutine(this.AnimateText(text));
    }

    private IEnumerator AnimateText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            // Animate text
            this.m_ChatboxText.text = text.Substring(0, i + 1);
            this.m_ChatboxText.ForceMeshUpdate();
            yield return new WaitForSeconds(this.m_TypingSpeed);
            
            // Update chatbox rect height
            this.m_ChatboxRect.sizeDelta =
                new Vector2(this.m_ChatboxRect.sizeDelta.x, this.m_ChatboxText.preferredHeight);
            yield return new WaitForEndOfFrame();

            // Only scroll if content is overflowing
            bool contentOverflows = this.m_ChatboxScroll.content.rect.height > this.m_ChatboxScroll.viewport.rect.height;
            if (contentOverflows)
                this.m_ChatboxScroll.verticalNormalizedPosition = 0f; // Scroll to bottom
        }

        yield return new WaitForSeconds(this.m_PostTextLifetime);
        this.HideGUI();
    }

    public void ShowBackground()
        => this.m_Background.gameObject.SetActive(true);

    public void HideBackground()
        => this.m_Background.gameObject.SetActive(false);

    public void ShowGUI() => 
        this.m_ContentGroup.SetActive(true);

    public void HideGUI() 
        => this.m_ContentGroup.SetActive(false);

    #endregion Methods

}
