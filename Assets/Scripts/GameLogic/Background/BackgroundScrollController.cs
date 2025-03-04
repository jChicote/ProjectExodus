using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.GameLogic.Pause.PausableMonoBehavior;
using UnityEngine;

namespace ProjectExodus
{

    public class BackgroundScrollController : PausableMonoBehavior
    {

        #region - - - - - - Fields - - - - - -

        public Sprite m_BackgroundTile;
        public Vector2 m_BackgroundTileSize;
        public float paralaxSpeed;
        
        [SerializeField] private List<BackgroundRowElement> m_Rows;

        private Transform m_CameraTransform;

        private int bottomIndex;
        private int upIndex;
        private float lastCameraY;
        
        private float lastCameraX;
        
        // Test inputs
        private int m_LeftIndex;
        private int m_RightIndex;
        private float m_TileWidth;
        private float m_TileHeight;

        private float m_LeftColumnDetectionHorizontalBoundary;
        private float m_RightColumnDetectionHorizontalBoundary;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_CameraTransform = Camera.main.transform;
            // lastCameraX = cameraTransform.position.x;
            // m_Columns = new List<Transform>transform.childCount];
            // for (int i = 0; i < transform.childCount; i++)
            // {
            //     m_Columns[i] = transform.GetChild(i);
            // }

            // leftIndex = 0;
            // rightIndex = m_Columns.Count - 1;

            // Get sprite properties
            Vector2 spriteSize = this.m_BackgroundTile.rect.size; // Size in pixels
            float pixelsPerUnit = this.m_BackgroundTile.pixelsPerUnit;

            // Convert to world space
            this.m_TileWidth = (spriteSize.x / pixelsPerUnit) * transform.lossyScale.x;
            this.m_TileHeight = (spriteSize.y / pixelsPerUnit) * transform.lossyScale.y;

            Debug.Log($"Sprite World Size: Width = {m_TileWidth}, Height = {m_TileHeight}");

            // TODO: Switch to calculated indexes
            this.m_LeftIndex = 0;
            this.m_RightIndex = 2;
            
            this.HorizontallyPositionColumnsOnStart();
            
            // Use the first row to calculate boundaries
            this.m_LeftColumnDetectionHorizontalBoundary =
                this.m_Rows[0].RowColums[0].position.x + this.m_TileWidth / 2;
            this.m_RightColumnDetectionHorizontalBoundary =
                this.m_Rows[0].RowColums[2].position.x - this.m_TileWidth / 2;
        }

        private void Update()
        {
            this.UpdateHorizontalScroll();
        }

        #endregion Unity Methods

        #region - - - - - - Horizontal Scroll Methods - - - - - -

        // private void UpdateHorizontalScroll()
        // {
        //     float deltaX = cameraTransform.position.x - lastCameraX;
        //     transform.position += Vector3.right * (deltaX * paralaxSpeed);
        //     lastCameraX = cameraTransform.position.x;
        // }

        private void HorizontallyPositionColumnsOnStart()
        {
            for (int _I = 0; _I < this.m_Rows.Count; _I++)
            {
                BackgroundRowElement _RowElement = this.m_Rows[_I];
                
                float _StartingColumnPositionX = this.m_CameraTransform.position.x - this.m_TileWidth;
                for (int _Y = 0; _Y < _RowElement.RowColums.Count; _Y++)
                {
                    // The vertical calculation is limited to only tile diameters of 3.
                    _RowElement.RowColums[_Y].position = new Vector2(
                        _StartingColumnPositionX,
                        this.m_CameraTransform.position.y + (this.m_TileHeight + -(_I * this.m_TileHeight)));
                    _StartingColumnPositionX += this.m_TileWidth;
                }
            }
        }

        // private void VerticallyPositionColumnsOnStart()
        // {
        //     
        // }

        private void UpdateHorizontalScroll()
        {
            if (this.m_CameraTransform.position.x < this.m_LeftColumnDetectionHorizontalBoundary)
                this.ScrollLeft();
            
            if (this.m_CameraTransform.position.x > this.m_RightColumnDetectionHorizontalBoundary)
                this.ScrollRight();
        }

        private void ScrollLeft()
        {
            List<Transform> _ReferenceRowColumns = this.m_Rows.First().RowColums;
            float _LeftColumnHorizontalPosition = _ReferenceRowColumns.ElementAt(this.m_LeftIndex).position.x;
            List<Transform> _RightColumnElements = this.m_Rows
                .Select(r => r.RowColums.ElementAt(this.m_RightIndex))
                .ToList();

            foreach (Transform _ColumnElement in _RightColumnElements)
                _ColumnElement.position = new Vector2(
                    _LeftColumnHorizontalPosition - this.m_TileWidth,
                    _ColumnElement.position.y);
            
            this.m_LeftIndex = this.m_RightIndex;
            this.m_RightIndex--;

            if (this.m_RightIndex < 0)
                this.m_RightIndex = _ReferenceRowColumns.Count - 1;
            
            this.RecalculateHorizontalBoundaries(_ReferenceRowColumns);
        }

        private void ScrollRight()
        {
            List<Transform> _ReferenceRowColumns = this.m_Rows.First().RowColums;
            float _RightColumnHorizontalPosition =
                _ReferenceRowColumns.ElementAt(this.m_RightIndex).position.x;
            List<Transform> _LeftColumnElements = this.m_Rows
                .Select(r => r.RowColums.ElementAt(this.m_LeftIndex))
                .ToList();

            foreach (Transform _ColumnElement in _LeftColumnElements)
                _ColumnElement.position = new Vector2(
                    _RightColumnHorizontalPosition + this.m_TileWidth,
                    _ColumnElement.position.y);

            this.m_RightIndex = this.m_LeftIndex;
            this.m_LeftIndex++;

            if (this.m_LeftIndex == _ReferenceRowColumns.Count)
                this.m_LeftIndex = 0;

            this.RecalculateHorizontalBoundaries(_ReferenceRowColumns);
        }

        private void RecalculateHorizontalBoundaries(List<Transform> referenceColumns)
        {
            this.m_LeftColumnDetectionHorizontalBoundary =
                referenceColumns[this.m_LeftIndex].position.x + this.m_TileWidth / 2;
            this.m_RightColumnDetectionHorizontalBoundary =
                referenceColumns[this.m_RightIndex].position.x - this.m_TileWidth / 2;
        }

        #endregion Horizontal Scroll Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            // Draw tile borders
            Gizmos.color = Color.yellow;
            Transform[] _BackgroundTiles = this.m_Rows.SelectMany(r => r.RowColums).ToArray();
            foreach (Transform _Tile in _BackgroundTiles)
                Gizmos.DrawWireCube(_Tile.position, new Vector2(this.m_TileWidth, this.m_TileHeight));

            // Draw boundary lines
            if (this.m_CameraTransform != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(
                    new Vector3(this.m_RightColumnDetectionHorizontalBoundary, this.m_CameraTransform.position.y - 90, 0),
                    new Vector3(this.m_RightColumnDetectionHorizontalBoundary, this.m_CameraTransform.position.y + 90, 0));
                Gizmos.DrawLine(
                    new Vector3(this.m_LeftColumnDetectionHorizontalBoundary, this.m_CameraTransform.position.y - 90, 0),
                    new Vector3(this.m_LeftColumnDetectionHorizontalBoundary, this.m_CameraTransform.position.y + 90, 0));
            }
        }

        #endregion Gizmos
  
    }

    [Serializable]
    public class BackgroundRowElement
    {

        #region - - - - - - Fields - - - - - -

        public Transform Row;
        public List<Transform> RowColums;

        #endregion Fields

    }

}