//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Style.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DungeonGenerator.Configuration
{
    using System.Collections.Generic;
    using DungeonGenerator.Enum;
    using UnityEngine;

    /// <summary>Define a style of a dungeon.</summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Dungeon", menuName = "Game/New Dungeon")]
    public class Style : ScriptableObject
    {
        [Header("Name:")]

        /// <summary>The name style</summary>
        [SerializeField]
        private string nameStyle = string.Empty;

        [Header("Floors:")]

        /// <summary>The floor</summary>
        [SerializeField]
        private List<GameObject> floors = new List<GameObject>();

        [Header("Walls:")]

        /// <summary>The wall down</summary>
        [SerializeField]
        private List<GameObject> wallsDown = new List<GameObject>();

        /// <summary>The wall left</summary>
        [SerializeField]
        private List<GameObject> wallsLeft = new List<GameObject>();

        /// <summary>The wall right</summary>
        [SerializeField]
        private List<GameObject> wallsRight = new List<GameObject>();

        /// <summary>The wall top</summary>
        [SerializeField]
        private List<GameObject> wallsTop = new List<GameObject>();

        [Header("Corners:")]

        /// <summary>The corner left down</summary>
        [SerializeField]
        private List<GameObject> cornersLeftDown = new List<GameObject>();

        /// <summary>The corner right down</summary>
        [SerializeField]
        private List<GameObject> cornersRightDown = new List<GameObject>();

        /// <summary>The corner left up</summary>
        [SerializeField]
        private List<GameObject> cornersLeftUp = new List<GameObject>();

        /// <summary>The corner right up</summary>
        [SerializeField]
        private List<GameObject> cornersRightUp = new List<GameObject>();

        [Header("Internal Corners:")]

        /// <summary>The corner internal left down</summary>
        [SerializeField]
        private List<GameObject> cornersInternalLeftDown = new List<GameObject>();

        /// <summary>The corner internal left up</summary>
        [SerializeField]
        private List<GameObject> cornersInternalLeftUp = new List<GameObject>();

        /// <summary>The corner internal right down</summary>
        [SerializeField]
        private List<GameObject> cornersInternalRightDown = new List<GameObject>();

        /// <summary>The corner internal right up</summary>
        [SerializeField]
        private List<GameObject> cornersInternalRightUp = new List<GameObject>();

        [Header("Decoration")]

        /// <summary>The decorations</summary>
        [SerializeField]
        private List<DecoMenu> decorations = new List<DecoMenu>();

        #region Encapsulate Fields

        /// <summary>Gets or sets the name style.</summary>
        /// <value>The name style.</value>
        public string NameStyle { get => nameStyle; set => nameStyle = value; }
        
        /// <summary>Gets or sets the floors.</summary>
        /// <value>The floors.</value>
        public List<GameObject> Floors { get => floors; set => floors = value; }
        
        /// <summary>Gets or sets the walls down.</summary>
        /// <value>The walls down.</value>
        public List<GameObject> WallsDown { get => wallsDown; set => wallsDown = value; }
        
        /// <summary>Gets or sets the walls left.</summary>
        /// <value>The walls left.</value>
        public List<GameObject> WallsLeft { get => wallsLeft; set => wallsLeft = value; }
        
        /// <summary>Gets or sets the walls right.</summary>
        /// <value>The walls right.</value>
        public List<GameObject> WallsRight { get => wallsRight; set => wallsRight = value; }
        
        /// <summary>Gets or sets the walls top.</summary>
        /// <value>The walls top.</value>
        public List<GameObject> WallsTop { get => wallsTop; set => wallsTop = value; }
        
        /// <summary>Gets or sets the corners left down.</summary>
        /// <value>The corners left down.</value>
        public List<GameObject> CornersLeftDown { get => cornersLeftDown; set => cornersLeftDown = value; }
        
        /// <summary>Gets or sets the corners right down.</summary>
        /// <value>The corners right down.</value>
        public List<GameObject> CornersRightDown { get => cornersRightDown; set => cornersRightDown = value; }
        
        /// <summary>Gets or sets the corners left up.</summary>
        /// <value>The corners left up.</value>
        public List<GameObject> CornersLeftUp { get => cornersLeftUp; set => cornersLeftUp = value; }
        
        /// <summary>Gets or sets the corners right up.</summary>
        /// <value>The corners right up.</value>
        public List<GameObject> CornersRightUp { get => cornersRightUp; set => cornersRightUp = value; }
       
        /// <summary>Gets or sets the corners internal left down.</summary>
        /// <value>The corners internal left down.</value>
        public List<GameObject> CornersInternalLeftDown { get => cornersInternalLeftDown; set => cornersInternalLeftDown = value; }
        
        /// <summary>Gets or sets the corners internal left up.</summary>
        /// <value>The corners internal left up.</value>
        public List<GameObject> CornersInternalLeftUp { get => cornersInternalLeftUp; set => cornersInternalLeftUp = value; }
        
        /// <summary>Gets or sets the corners internal right down.</summary>
        /// <value>The corners internal right down.</value>
        public List<GameObject> CornersInternalRightDown { get => cornersInternalRightDown; set => cornersInternalRightDown = value; }
        
        /// <summary>Gets or sets the corners internal right up.</summary>
        /// <value>The corners internal right up.</value>
        public List<GameObject> CornersInternalRightUp { get => cornersInternalRightUp; set => cornersInternalRightUp = value; }
        
        /// <summary>Gets or sets the decorations.</summary>
        /// <value>The decorations.</value>
        public List<DecoMenu> Decorations { get => decorations; set => decorations = value; }

        #endregion

        /// <summary>Gets the tile.</summary>
        /// <param name="tileBox">The tile box.</param>
        /// <returns>Return the texture.</returns>
        public GameObject GetTile(Box tileBox)
        {
            return RandomGameObject(
                tileBox.Equals(Box.WallDown) ? WallsDown :
                tileBox.Equals(Box.WallLeft) ? WallsLeft :
                tileBox.Equals(Box.WallRight) ? WallsRight :
                tileBox.Equals(Box.WallTop) ? WallsTop :
                tileBox.Equals(Box.CornerLeftUp) ? CornersLeftUp :
                tileBox.Equals(Box.CornerRightUp) ? CornersRightUp :
                tileBox.Equals(Box.CornerLeftDown) ? CornersLeftDown :
                tileBox.Equals(Box.CornerRightDown) ? CornersRightDown :
                tileBox.Equals(Box.CornerInternalLeftDown) ? CornersInternalLeftDown :
                tileBox.Equals(Box.CornerInternalLeftUp) ? CornersInternalLeftUp :
                tileBox.Equals(Box.CornerInternalRightDown) ? CornersInternalRightDown :
                tileBox.Equals(Box.CornerInternalRightUp) ? CornersInternalRightUp :
                Floors);
        }

        /// <summary>Random the game object.</summary>
        /// <param name="gameObjects">The game objects.</param>
        /// <returns>A game object.</returns>
        private GameObject RandomGameObject(List<GameObject> gameObjects) 
        {
            return gameObjects[Random.Range(0, gameObjects.Count - 1)];
        }
    }
}