using System;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    private bool m_ShowConsole;
    private bool m_ShowHelp;
    private string m_Input;
    private DebugManager m_DebugManager;
    
    private Vector2 m_Scroll;
    private GUIStyle m_DarkBackgroundStyle;

    #endregion Fields
  
    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_DebugManager = DebugManager.Instance;
        
        // Create a new GUIStyle
        this.m_DarkBackgroundStyle = new GUIStyle();

        // Create a black texture
        Texture2D _BlackTexture = new Texture2D(1, 1);
        _BlackTexture.SetPixel(0, 0, new Color(0, 0, 0, 0.8f));
        _BlackTexture.Apply();

        // Assign the texture as the background for the GUIStyle
        this.m_DarkBackgroundStyle.normal.background = _BlackTexture;
    }

    #endregion Unity Methods

    #region - - - - - - Methods - - - - - -

    public void OnConsolePressed()
    {
        this.m_ShowConsole = !this.m_ShowConsole;

        if (!this.m_ShowConsole)
            this.m_ShowHelp = false;

        // Cursor.visible = this.m_ShowConsole;
    }

    public void ShowHelp(bool showHelp) 
        => this.m_ShowHelp = showHelp;

    public void OnSubmit()
    {
        if(this.m_ShowConsole)
            this.HandleInput();
    }

    private void HandleInput()
    {
        if (string.IsNullOrWhiteSpace(this.m_Input)) return;

        string[] _Inputs = this.m_Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var _TargetCommand = DebugManager.Instance.CommandList.Find(c =>
            c is DebugCommandBase _CommandBase && _Inputs[0] == _CommandBase.CommandID);

        if (_TargetCommand != null)
        {
            // If needing to handle different type parameters, tack on to include other DebugCommand overloads.
            if (_TargetCommand as DebugCommand != null)
                (_TargetCommand as DebugCommand).Invoke();
        }

        this.m_Input = string.Empty;
    }

    #endregion Methods

    #region - - - - - - GUI Methods - - - - - -

    private void OnGUI()
    {
        if (!this.m_ShowConsole) return;

        float _Y = 0f;

        // Show help menu
        if (this.m_ShowHelp)
        {
            GUI.Box(new Rect(0, _Y, Screen.width, 100), string.Empty, this.m_DarkBackgroundStyle);

            Rect _ViewPort = new Rect(0, 0, Screen.width - 30, 20 * this.m_DebugManager.CommandList.Count);
            this.m_Scroll = GUI.BeginScrollView(new Rect(0, _Y + 5f, Screen.width, 90), this.m_Scroll, _ViewPort);

            for (int _I = 0; _I < this.m_DebugManager.CommandList.Count; _I++)
            {
                DebugCommandBase _Command = this.m_DebugManager.CommandList[_I] as DebugCommandBase;
                string _Label = $"{_Command.CommandFormat} - {_Command.CommandDescription}";
                Rect _LabelRect = new Rect(5, 20 * _I, _ViewPort.width - 100, 20);
                GUI.Label(_LabelRect, _Label);                
            }
            
            GUI.EndScrollView();
            _Y += 100;
        }
        
        GUI.Box(new Rect(0, _Y, Screen.width, 30), string.Empty, this.m_DarkBackgroundStyle);
        GUI.backgroundColor = new Color(0, 0, 0, 0.8f);

        this.m_Input = GUI.TextField(new Rect(10f, _Y + 5f, Screen.width - 20f, 20f), this.m_Input);
    }

    #endregion GUI Methods
  
}
