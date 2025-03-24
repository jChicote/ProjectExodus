using TMPro;
using UnityEngine;

public class ChatboxHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public GameObject m_ContentGroup;
    public TMP_Text m_ChatboxText;

    #endregion Fields

    #region - - - - - - Methods - - - - - -

    public void SetChatText(string text)
    {
        this.m_ChatboxText.text = text;
    }

    public void ShowGUI() => 
        this.m_ContentGroup.SetActive(true);

    public void HideGUI() 
        => this.m_ContentGroup.SetActive(false);

    #endregion Methods

}
