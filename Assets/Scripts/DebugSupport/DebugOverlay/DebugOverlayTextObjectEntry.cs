using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Contains the text representation for a given value to debug.
/// </summary>
public class DebugOverlayTextObjectEntry : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public TMP_Text Label;

    private string m_Title;
    private Func<object> m_ValueCallback;

    #endregion Fields

    #region - - - - - - Properties - - - - - -

    public string Title => this.m_Title;

    #endregion Properties
  
    #region - - - - - - Methods - - - - - -

    public void Initialise(string labelTitle, Func<object> valueCallback)
    {
        this.m_Title = labelTitle;
        this.m_ValueCallback = valueCallback;
    }

    private void Update()
    {
        if (this.m_ValueCallback == null) return;

        var _Result = this.m_ValueCallback.Invoke();
        this.Label.text = $"{this.m_Title}: {_Result}";
    }

    #endregion Methods

}
