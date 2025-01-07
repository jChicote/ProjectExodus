using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Responsible for managing the overlayed debug entries to present from the game scene. 
/// </summary>
public class DebugOverlayer : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public GameObject DebugPanel;
    public GameObject DebugEntryTemplate;
    public List<DebugOverlayTextObjectEntry> m_DebugEntries;

    private int fontSize;

    #endregion Fields
  
    #region - - - - - - Methods - - - - - -

    public void AddEntry(string entryTitle, Func<object> callback)
    {
        DebugOverlayTextObjectEntry _NewEntry = Instantiate(
            this.DebugEntryTemplate, 
            this.DebugPanel.transform).GetComponent<DebugOverlayTextObjectEntry>();
        _NewEntry.Initialise(entryTitle, callback);
        this.m_DebugEntries.Add(_NewEntry);
    }

    public void RemoveEntry(string entryTitle)
    {
        DebugOverlayTextObjectEntry _EntryToRemove = this.m_DebugEntries.First(de => de.Title == entryTitle);
        this.m_DebugEntries.Remove(_EntryToRemove);
        Destroy(_EntryToRemove.gameObject);
    }

    public void ClearEntries()
    {
        List<DebugOverlayTextObjectEntry> _EntriesToClear = this.m_DebugEntries.ToList();
        
        this.m_DebugEntries.Clear();
        foreach (DebugOverlayTextObjectEntry _Entry in _EntriesToClear)
            Destroy(_Entry.gameObject);
    }

    #endregion Methods
  
}
