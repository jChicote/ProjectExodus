using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Contains the text representation for a given value to debug.
/// </summary>
public class DebugOverlayTextObjectEntry : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public TMP_Text OverlayEntryText;

    private string m_Title;
    private Func<DebugOverlayInterfaceElements, object> m_ValueCallback;
    private DebugOverlayInterfaceElements m_OverlayInterfaceElements;

    #endregion Fields

    #region - - - - - - Properties - - - - - -

    public string Title => this.m_Title;

    #endregion Properties
  
    #region - - - - - - Methods - - - - - -

    public void Initialise(string labelTitle, Func<DebugOverlayInterfaceElements, object> valueCallback)
    {
        this.m_Title = labelTitle;
        this.m_ValueCallback = valueCallback;

        this.m_OverlayInterfaceElements = new DebugOverlayInterfaceElements(this.OverlayEntryText);
    }

    private void Update()
    {
        if (this.m_ValueCallback == null) return;

        var _Result = this.m_ValueCallback.Invoke(this.m_OverlayInterfaceElements);
        this.OverlayEntryText.text = $"{this.m_Title}: {_Result}";
    }

    #endregion Methods

}

public class DebugOverlayInterfaceElements
{

    #region - - - - - - Properties - - - - - -

    public TMP_Text OverlayEntryText { get; private set; }

    #endregion Properties

    #region - - - - - - Constructors - - - - - -

    public DebugOverlayInterfaceElements(TMP_Text overlayEntryText) 
        => this.OverlayEntryText = overlayEntryText;

    #endregion Constructors
  
}
