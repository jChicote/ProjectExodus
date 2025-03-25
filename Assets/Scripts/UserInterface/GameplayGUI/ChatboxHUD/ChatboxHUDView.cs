using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatboxHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public GameObject m_ContentGroup;
    public TMP_Text m_ChatboxText;
    public ScrollRect m_ChatboxScroll;
    public float m_TypingSpeed = 0.05f;

    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_ChatboxText.text = "";
    }

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
            this.m_ChatboxText.text = text.Substring(0, i + 1);
            this.m_ChatboxScroll.verticalNormalizedPosition = 0;
            yield return new WaitForSeconds(this.m_TypingSpeed);
        }
    }

    public void ShowGUI() => 
        this.m_ContentGroup.SetActive(true);

    public void HideGUI() 
        => this.m_ContentGroup.SetActive(false);

    #endregion Methods

}
