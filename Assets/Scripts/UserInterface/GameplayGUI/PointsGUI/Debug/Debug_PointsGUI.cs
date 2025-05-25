using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;

public class Debug_PointsGUI : IDebugCommandRegistrater
{

    #region - - - - - - Methods - - - - - -

    public void RegisterCommand(IDebugCommandSystem debugCommandSystem)
    {
        DebugCommand _SpawnPointsMarker = new DebugCommand(
            "ui_spawnpointsmarker",
            "Spawn single points marker with randomised number of points & position.",
            "ui_spawnpointsmarker",
            this.SpawnPointsMarker);
        DebugCommand _UpdateTotalPoints = new DebugCommand(
            "ui_updatetotalpoints",
            "Updates Total Points text.",
            "ui_updatetotalpoints",
            this.UpdateTotalPoints);
            
        debugCommandSystem.RegisterCommand(_SpawnPointsMarker);
        debugCommandSystem.RegisterCommand(_UpdateTotalPoints);
    }

    private void SpawnPointsMarker()
    {
        int _RandomisedPoints = Random.Range(10, 10000);
        Vector2 _RandomPosition = this.GenerateRandomPositionWithinView();
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(PointsGUIConstants.AddPoints, new PointsInfo
        {
            Points = _RandomisedPoints,
            Position = _RandomPosition
        });
    }

    private void UpdateTotalPoints()
    {
        int _RandomisedPoints = Random.Range(10, 100000);
        IUIEventMediator _EventMediator = UserInterfaceManager.Instance.EventMediator;
        _EventMediator.Dispatch(PointsGUIConstants.UpdatePoints, _RandomisedPoints);
    }
    
    // TODO: Please move this to its own class and possibly as an extension.
    private Vector2 GenerateRandomPositionWithinView()
    {
        Camera _MainCamera = Camera.main;
        float _Width = 10f;
        float _Height = 10f;

        Vector2 _RandomPosition = new(
            x: UnityEngine.Random.Range(_MainCamera.transform.position.x - _Width / 2,
                _MainCamera.transform.position.y + _Width / 2),
            y: UnityEngine.Random.Range(_MainCamera.transform.position.y - _Height / 2,
                _MainCamera.transform.position.y + _Height / 2));
        return _RandomPosition;
    }

    #endregion Methods
  
}
