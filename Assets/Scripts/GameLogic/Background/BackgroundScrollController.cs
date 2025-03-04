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

        private float m_HorizontalViewZone = 6f;
        private float m_VerticalViewZone = 3f;
        
        private int bottomIndex;
        private int upIndex;
        private float lastCameraY;
        
        private int leftIndex;
        private int rightIndex;
        private float lastCameraX;
        
        // Test inputs
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

            leftIndex = 0;
            // rightIndex = m_Columns.Count - 1;

            // Get sprite properties
            Vector2 spriteSize = this.m_BackgroundTile.rect.size; // Size in pixels
            float pixelsPerUnit = this.m_BackgroundTile.pixelsPerUnit;

            // Convert to world space
            this.m_TileWidth = (spriteSize.x / pixelsPerUnit) * transform.lossyScale.x;
            this.m_TileHeight = (spriteSize.y / pixelsPerUnit) * transform.lossyScale.y;

            Debug.Log($"Sprite World Size: Width = {m_TileWidth}, Height = {m_TileHeight}");
            
            // Use the first row to calculate boundaries
            this.m_LeftColumnDetectionHorizontalBoundary =
                this.m_Rows[0].RowColums[0].position.x + this.m_TileWidth / 2;
            this.m_RightColumnDetectionHorizontalBoundary =
                this.m_Rows[0].RowColums[2].position.x - this.m_TileWidth / 2;
            
            this.HorizontallyPositionColumnsOnStart();
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
        //
        //     if (cameraTransform.position.x < (m_Columns[leftIndex].transform.position.x + this.m_TileWidth))
        //         this.ScrollingLeft();
        //
        //     if (cameraTransform.position.x > (m_Columns[rightIndex].transform.position.x) - this.m_TileWidth)
        //         this.ScrollingRight();
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
            
        }

        private void ScrollRight()
        {
            
        }

        // private void ScrollingLeft()
        // {
        //     m_Columns[rightIndex].position = new Vector3(
        //         m_Columns[leftIndex].position.x - this.m_BackgroundTileSize.x, 
        //         1 * m_Columns[leftIndex].position.y, 0);
        //     //layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        //     leftIndex = rightIndex;
        //     rightIndex--;
        //     
        //     if (rightIndex < 0)
        //         rightIndex = m_Columns.Count - 1;
        // }
        //
        // private void ScrollingRight()
        // {
        //     m_Columns[leftIndex].position = new Vector3(
        //         m_Columns[rightIndex].position.x + this.m_BackgroundTileSize.x, 
        //         1 * m_Columns[rightIndex].position.y, 0);
        //     //layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        //     rightIndex = leftIndex;
        //     leftIndex++;
        //     
        //     if (leftIndex == m_Columns.Count)
        //         leftIndex = 0;
        // }

        #endregion Horizontal Scroll Methods

        #region - - - - - - Gizmos - - - - - -

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Transform[] _BackgroundTiles = this.m_Rows.SelectMany(r => r.RowColums).ToArray();
            foreach (Transform _Tile in _BackgroundTiles)
                Gizmos.DrawWireCube(_Tile.position, new Vector2(this.m_TileWidth, this.m_TileHeight));
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