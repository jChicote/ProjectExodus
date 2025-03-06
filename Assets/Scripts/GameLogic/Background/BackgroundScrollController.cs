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

        [SerializeField] private Sprite m_BackgroundTile;
        [SerializeField] private List<BackgroundRowElement> m_Rows;

        private Transform m_CameraTransform;
        
        // Tile-Info Fields
        private float m_TileWidth;
        private float m_TileHeight;
        
        // Runtime Scroll Fields
        private int m_LeftIndex;
        private int m_RightIndex;
        private int m_BottomIndex;
        private int m_TopIndex;
        private float m_LeftScrollTriggerLimit;
        private float m_RightScrollTriggerLimit;
        private float m_LowerScrollTriggerLimit;
        private float m_UpperScrollTriggerLimit;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start()
        {
            this.m_CameraTransform = Camera.main.transform;
            
            // Get sprite properties
            Vector2 _SpriteSize = this.m_BackgroundTile.rect.size; // Size in pixels
            float _PixelsPerUnit = this.m_BackgroundTile.pixelsPerUnit;

            // Convert to world space
            this.m_TileWidth = (_SpriteSize.x / _PixelsPerUnit) * transform.lossyScale.x;
            this.m_TileHeight = (_SpriteSize.y / _PixelsPerUnit) * transform.lossyScale.y;

            // Prepare tiles
            this.m_LeftIndex = 0;
            this.m_RightIndex = this.m_Rows.First().RowColums.Count - 1;
            this.m_BottomIndex = this.m_Rows.Count - 1;
            this.m_TopIndex = 0;
            
            this.PositionRowColumnsOnStart();
            
            // Use the first row to calculate boundaries
            this.RecalculateHorizontalBoundaries(this.m_Rows.First().RowColums);
            this.RecalculateVerticalBoundaries();
        }

        private void Update()
        {
            this.UpdateHorizontalScroll();
            this.UpdateVerticalScroll();
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        private void PositionRowColumnsOnStart()
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

        #endregion Methods
  
        #region - - - - - - Horizontal Scroll Methods - - - - - -

        private void UpdateHorizontalScroll()
        {
            if (this.m_CameraTransform.position.x < this.m_LeftScrollTriggerLimit)
                this.ScrollLeft();
            
            if (this.m_CameraTransform.position.x > this.m_RightScrollTriggerLimit)
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
            this.m_LeftScrollTriggerLimit =
                referenceColumns[this.m_LeftIndex].position.x + this.m_TileWidth / 2;
            this.m_RightScrollTriggerLimit =
                referenceColumns[this.m_RightIndex].position.x - this.m_TileWidth / 2;
        }

        #endregion Horizontal Scroll Methods

        #region - - - - - - Vertical Scroll Methods - - - - - -

        private void UpdateVerticalScroll()
        {
            if (this.m_CameraTransform.position.y < this.m_LowerScrollTriggerLimit)
                this.ScrollDown();
            
            if (this.m_CameraTransform.position.y > this.m_UpperScrollTriggerLimit)
                this.ScrollUp();
        }

        private void ScrollUp()
        {
            float _TopRowYPosition = this.m_Rows[this.m_TopIndex].RowColums[0].position.y;
            List<Transform> _BottomRows = this.m_Rows[this.m_BottomIndex].RowColums;

            foreach (Transform _RowColumnElement in _BottomRows)
                _RowColumnElement.position = new Vector2(
                    _RowColumnElement.position.x,
                    _TopRowYPosition + this.m_TileHeight);

            this.m_TopIndex = this.m_BottomIndex;
            this.m_BottomIndex--;
            
            if (this.m_BottomIndex < 0)
                this.m_BottomIndex = this.m_Rows.Count - 1;

            this.RecalculateVerticalBoundaries();
        }

        private void ScrollDown()
        {
            float _BottonRowYPosition = this.m_Rows[this.m_BottomIndex].RowColums[0].position.y;
            List<Transform> _TopRows = this.m_Rows[this.m_TopIndex].RowColums;
            
            foreach (Transform _RowColumnElement in _TopRows)
                _RowColumnElement.position = new Vector2(
                    _RowColumnElement.position.x,
                    _BottonRowYPosition - this.m_TileHeight);

            this.m_BottomIndex = this.m_TopIndex;
            this.m_TopIndex++;
            
            if (this.m_TopIndex == this.m_Rows.Count)
                this.m_TopIndex = 0;
            
            this.RecalculateVerticalBoundaries();
        }

        private void RecalculateVerticalBoundaries()
        {
            this.m_LowerScrollTriggerLimit =
                this.m_Rows[this.m_BottomIndex].RowColums[0].position.y + this.m_TileHeight / 2;
            this.m_UpperScrollTriggerLimit =
                this.m_Rows[this.m_TopIndex].RowColums[0].position.y - this.m_TileHeight / 2;
        }

        #endregion Vertical Scroll Methods
  
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
                
                // Draw Horizontal boundaries
                Gizmos.DrawLine(
                    new Vector3(this.m_RightScrollTriggerLimit, this.m_CameraTransform.position.y - 90, 0),
                    new Vector3(this.m_RightScrollTriggerLimit, this.m_CameraTransform.position.y + 90, 0));
                Gizmos.DrawLine(
                    new Vector3(this.m_LeftScrollTriggerLimit, this.m_CameraTransform.position.y - 90, 0),
                    new Vector3(this.m_LeftScrollTriggerLimit, this.m_CameraTransform.position.y + 90, 0));
                
                // Draw Vertical boundaries
                Gizmos.DrawLine(
                    new Vector3(this.m_CameraTransform.position.x - 90, this.m_UpperScrollTriggerLimit, 0),
                    new Vector3(this.m_CameraTransform.position.x + 90, this.m_UpperScrollTriggerLimit, 0));
                Gizmos.DrawLine(
                    new Vector3(this.m_CameraTransform.position.x - 90, this.m_LowerScrollTriggerLimit, 0),
                    new Vector3(this.m_CameraTransform.position.x + 90, this.m_LowerScrollTriggerLimit, 0));
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