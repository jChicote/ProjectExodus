using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyLocatorHUDView : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    [SerializeField] private GameObject m_MarkerPrefab;

    [SerializeField] private GameObject m_CanvasGroup;
    private Dictionary<int, RectTransform> m_Markers = new();

    public  RectTransform m_CanvasRect;
    
    #endregion Fields

    #region - - - - - - Unity Methods - - - - - -

    private void Start()
    {
        this.m_CanvasRect = this.m_CanvasGroup.GetComponent<RectTransform>();
    }

    #endregion Unity Methods
  
    #region - - - - - - Methods - - - - - -

    public void AddMarker(int id)
    {
        GameObject _MarkerInstance = Instantiate(this.m_MarkerPrefab, this.m_CanvasGroup.transform);
        _MarkerInstance.SetActive(true);
        this.m_Markers.Add(id, _MarkerInstance.GetComponent<RectTransform>());
    }

    public Vector2 GetCanvasSizeDelta()
    {
        return this.m_CanvasRect.sizeDelta;
    }

    public void UpdateMarker(int id, Vector2 newScreenPosition)
    {
        RectTransform _Marker = this.m_Markers[id];
        Debug.Log(newScreenPosition);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.m_CanvasRect,
            newScreenPosition,
            Camera.main,
            out Vector2 _LocalPoint);
        
        _Marker.anchoredPosition = _LocalPoint;
    }

    #endregion Methods
  
}
