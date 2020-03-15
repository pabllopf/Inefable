//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Style.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>Define a style of a dungeon.</summary>
[System.Serializable]
[CreateAssetMenu(fileName = "New Syle", menuName = "Dungeon/Style")]
public class Style : ScriptableObject
{
    [Header("Name:")]

    /// <summary>The name style</summary>
    [SerializeField]
    private string nameStyle = string.Empty;

    [Header("Floors:")]

    /// <summary>The floor</summary>
    [SerializeField]
    private List<TileMenu> floors = new List<TileMenu>();

    [Header("Walls:")]

    /// <summary>The wall down</summary>
    [SerializeField]
    private List<TileMenu> wallDown = new List<TileMenu>();

    /// <summary>The wall left</summary>
    [SerializeField]
    private List<TileMenu> wallLeft = new List<TileMenu>();

    /// <summary>The wall right</summary>
    [SerializeField]
    private List<TileMenu> wallRight = new List<TileMenu>();

    /// <summary>The wall top</summary>
    [SerializeField]
    private List<TileMenu> wallTop = new List<TileMenu>();

    [Header("Corners:")]

    /// <summary>The corner left down</summary>
    [SerializeField]
    private List<TileMenu> cornerLeftDown = new List<TileMenu>();

    /// <summary>The corner right down</summary>
    [SerializeField]
    private List<TileMenu> cornerRightDown = new List<TileMenu>();

    /// <summary>The corner left up</summary>
    [SerializeField]
    private List<TileMenu> cornerLeftUp = new List<TileMenu>();

    /// <summary>The corner right up</summary>
    [SerializeField]
    private List<TileMenu> cornerRightUp = new List<TileMenu>();

    [Header("Internal Corners:")]

    /// <summary>The corner internal left down</summary>
    [SerializeField]
    private List<TileMenu> cornerInternalLeftDown = new List<TileMenu>();

    /// <summary>The corner internal left up</summary>
    [SerializeField]
    private List<TileMenu> cornerInternalLeftUp = new List<TileMenu>();

    /// <summary>The corner internal right down</summary>
    [SerializeField]
    private List<TileMenu> cornerInternalRightDown = new List<TileMenu>();

    /// <summary>The corner internal right up</summary>
    [SerializeField]
    private List<TileMenu> cornerInternalRightUp = new List<TileMenu>();

    [Header("Decoration:")]

    /// <summary>The decoration</summary>
    [SerializeField]
    private List<DecoMenu> decorations = new List<DecoMenu>();

    #region Encapsulate Fields

    /// <summary>Gets or sets the name style.</summary>
    /// <value>The name style.</value>
    public string NameStyle { get => nameStyle; set => nameStyle = value; }

    /// <summary>Gets the floor.</summary>
    /// <value>The floor.</value>
    public GameObject Floor { get => floors[Random.Range(0, floors.Count - 1)].Prefab; }

    /// <summary>Gets the wall down.</summary>
    /// <value>The wall down.</value>
    public GameObject WallDown { get => wallDown[Random.Range(0, wallDown.Count - 1)].Prefab; }

    /// <summary>Gets the wall left.</summary>
    /// <value>The wall left.</value>
    public GameObject WallLeft { get => wallLeft[Random.Range(0, wallLeft.Count - 1)].Prefab; }

    /// <summary>Gets the wall right.</summary>
    /// <value>The wall right.</value>
    public GameObject WallRight { get => wallRight[Random.Range(0, wallRight.Count - 1)].Prefab; }

    /// <summary>Gets the wall top.</summary>
    /// <value>The wall top.</value>
    public GameObject WallTop { get => wallTop[Random.Range(0, wallTop.Count - 1)].Prefab; }

    /// <summary>Gets the corner left down.</summary>
    /// <value>The corner left down.</value>
    public GameObject CornerLeftDown { get => cornerLeftDown[Random.Range(0, cornerLeftDown.Count - 1)].Prefab; }

    /// <summary>Gets the corner right down.</summary>
    /// <value>The corner right down.</value>
    public GameObject CornerRightDown { get => cornerRightDown[Random.Range(0, cornerRightDown.Count - 1)].Prefab; }

    /// <summary>Gets the corner left up.</summary>
    /// <value>The corner left up.</value>
    public GameObject CornerLeftUp { get => cornerLeftUp[Random.Range(0, cornerLeftUp.Count - 1)].Prefab; }

    /// <summary>Gets the corner right up.</summary>
    /// <value>The corner right up.</value>
    public GameObject CornerRightUp { get => cornerRightUp[Random.Range(0, cornerRightUp.Count - 1)].Prefab; }

    /// <summary>Gets the corner internal left down.</summary>
    /// <value>The corner internal left down.</value>
    public GameObject CornerInternalLeftDown { get => cornerInternalLeftDown[Random.Range(0, cornerInternalLeftDown.Count - 1)].Prefab; }

    /// <summary>Gets the corner internal left up.</summary>
    /// <value>The corner internal left up.</value>
    public GameObject CornerInternalLeftUp { get => cornerInternalLeftUp[Random.Range(0, cornerInternalLeftUp.Count - 1)].Prefab; }

    /// <summary>Gets the corner internal right down.</summary>
    /// <value>The corner internal right down.</value>
    public GameObject CornerInternalRightDown { get => cornerInternalRightDown[Random.Range(0, cornerInternalRightDown.Count - 1)].Prefab; }

    /// <summary>Gets the corner internal right up.</summary>
    /// <value>The corner internal right up.</value>
    public GameObject CornerInternalRightUp { get => cornerInternalRightUp[Random.Range(0, cornerInternalRightUp.Count - 1)].Prefab; }

    /// <summary>Gets or sets the decoration.</summary>
    /// <value>The decoration.</value>
    public List<DecoMenu> Decorations { get => decorations; set => decorations = value; }

    #endregion

    /// <summary>Gets the tile.</summary>
    /// <param name="tileBox">The tile box.</param>
    /// <returns>Return the texture.</returns>
    public GameObject GetTile(BoardBox tileBox)
    {
        return 
            tileBox.Equals(BoardBox.WallDown) ? WallDown :
            tileBox.Equals(BoardBox.WallLeft) ? WallLeft :
            tileBox.Equals(BoardBox.WallRight) ? WallRight :
            tileBox.Equals(BoardBox.WallTop) ? WallTop :
            tileBox.Equals(BoardBox.CornerLeftUp) ? CornerLeftUp :
            tileBox.Equals(BoardBox.CornerRightUp) ? CornerRightUp :
            tileBox.Equals(BoardBox.CornerLeftDown) ? CornerLeftDown :
            tileBox.Equals(BoardBox.CornerRightDown) ? CornerRightDown :
            tileBox.Equals(BoardBox.CornerInternalLeftDown) ? CornerInternalLeftDown :
            tileBox.Equals(BoardBox.CornerInternalLeftUp) ? CornerInternalLeftUp :
            tileBox.Equals(BoardBox.CornerInternalRightDown) ? CornerInternalRightDown :
            tileBox.Equals(BoardBox.CornerInternalRightUp) ? CornerInternalRightUp :
            Floor;
    }

    #region Validate Fields
#if UNITY_EDITOR
    /// <summary>Called when [validate].</summary>
    public void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            EditorUtility.SetDirty(this);
            UpdateDecoration();
        };

        UnityEditor.EditorApplication.delayCall += () =>
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, NameStyle);
            UpdateButton();

            AssetDatabase.SaveAssets();
        };

        UnityEditor.EditorApplication.delayCall += () =>
        {
            AssetDatabase.Refresh();
        };
    }

    /// <summary>Updates the button.</summary>
    public void UpdateButton() 
    {
        floors = UpdateFields(NameStyle + "/Board/Floor", ref floors);

        wallDown = UpdateFields(NameStyle + "/Board/WallDown", ref wallDown);
        wallLeft = UpdateFields(NameStyle + "/Board/WallLeft", ref wallLeft);
        wallRight = UpdateFields(NameStyle + "/Board/WallRight", ref wallRight);
        wallTop = UpdateFields(NameStyle + "/Board/WallTop", ref wallTop);

        cornerLeftDown = UpdateFields(NameStyle + "/Board/CornerLD", ref cornerLeftDown);
        cornerLeftUp = UpdateFields(NameStyle + "/Board/CornerLU", ref cornerLeftUp);
        cornerRightDown = UpdateFields(NameStyle + "/Board/CornerRD", ref cornerRightDown);
        cornerRightUp = UpdateFields(NameStyle + "/Board/CornerRU", ref cornerRightUp);

        cornerInternalLeftDown = UpdateFields(NameStyle + "/Board/CornerILD", ref cornerInternalLeftDown);
        cornerInternalLeftUp = UpdateFields(NameStyle + "/Board/CornerILU", ref cornerInternalLeftUp);
        cornerInternalRightDown = UpdateFields(NameStyle + "/Board/CornerIRD", ref cornerInternalRightDown);
        cornerInternalRightUp = UpdateFields(NameStyle + "/Board/CornerIRU", ref cornerInternalRightUp);
    }

    /// <summary>Updates the fields.</summary>
    /// <param name="path">The path.</param>
    /// <returns>Return list of tiles of dungeon.</returns>
    public List<TileMenu> UpdateFields(string path, ref List<TileMenu> list) 
    {
        string pathFiles = Application.dataPath + "/Prefabs/Dungeon/" + path;
        List<TileMenu> tiles = list;

        if (Directory.Exists(pathFiles)) 
        {
            Directory.GetFiles(pathFiles)
                .ToList()
                .FindAll(file => Path.GetExtension(file) == ".prefab")
                .ForEach(filePath =>
                {
                    string objPath = "Assets/Prefabs/Dungeon/" + path + "/" + Path.GetFileName(filePath);
                    GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(objPath, typeof(GameObject));

                    if (!tiles.Any(i => i.Prefab == obj))
                    {
                        TileMenu tileMenu = new TileMenu();
                        tileMenu.Prefab = obj;
                        tiles.Add(tileMenu);
                    }
                });
        }
        return tiles;
    }

    public void UpdateDecoration() 
    {
        DecoMenu[] decocor = new DecoMenu[Decorations.Count];
        Decorations.CopyTo(decocor);
        List<DecoMenu> decoMenuTempList = decocor.ToList();
        Decorations.Clear();

        string path = Application.dataPath + "/Prefabs/Dungeon/" + NameStyle + "/Decoration";
        Directory.GetFiles(path)
        .ToList()
        .ForEach(filePath =>
        {
            if (Path.GetExtension(filePath) == ".prefab")
            {
                string objPath = "Assets/Prefabs/Dungeon/" + NameStyle + "/Decoration/" + Path.GetFileName(filePath);

                GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(objPath, typeof(GameObject));

                if (obj.GetComponent<Decoration>())
                {
                    if (decoMenuTempList.Any(i => i.Prefab == obj.GetComponent<Decoration>().Prefab))
                    {
                        DecoMenu decoMenu = decoMenuTempList.Find(i => i.Prefab == obj.GetComponent<Decoration>().Prefab);
                        Decorations.Add(decoMenu);

                        Decoration deco = obj.GetComponent<Decoration>();
                        deco.Prefab = decoMenu.Prefab;
                        deco.BoxToSpawn = decoMenu.BoxToSpawn;
                        deco.MinToSpawn = decoMenu.MinToSpawn;
                        deco.MaxToSpawn = decoMenu.MaxToSpawn;

                        return;
                    }
                    else
                    {
                        DecoMenu decoMenu = new DecoMenu();
                        Decoration deco = obj.GetComponent<Decoration>();

                        decoMenu.Prefab = deco.Prefab;
                        decoMenu.BoxToSpawn = deco.BoxToSpawn;
                        decoMenu.MinToSpawn = deco.MinToSpawn;
                        decoMenu.MaxToSpawn = deco.MaxToSpawn;

                        Decorations.Add(decoMenu);
                    }
                }
            }
        });
    }

#endif
    #endregion
}