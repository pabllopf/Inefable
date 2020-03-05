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

    [Header("Textures:")]

    /// <summary>The floor</summary>
    [SerializeField]
    private GameObject floor = null;

    [Space(10)]

    /// <summary>The wall down</summary>
    [SerializeField]
    private GameObject wallDown = null;

    /// <summary>The wall left</summary>
    [SerializeField]
    private GameObject wallLeft = null;

    /// <summary>The wall right</summary>
    [SerializeField]
    private GameObject wallRight = null;

    /// <summary>The wall top</summary>
    [SerializeField]
    private GameObject wallTop = null;

    [Space(10)]

    /// <summary>The corner left down</summary>
    [SerializeField]
    private GameObject cornerLeftDown = null;

    /// <summary>The corner right down</summary>
    [SerializeField]
    private GameObject cornerRightDown = null;

    /// <summary>The corner left up</summary>
    [SerializeField]
    private GameObject cornerLeftUp = null;

    /// <summary>The corner right up</summary>
    [SerializeField]
    private GameObject cornerRightUp = null;

    [Space(10)]

    /// <summary>The corner internal left down</summary>
    [SerializeField]
    private GameObject cornerInternalLeftDown = null;

    /// <summary>The corner internal left up</summary>
    [SerializeField]
    private GameObject cornerInternalLeftUp = null;

    /// <summary>The corner internal right down</summary>
    [SerializeField]
    private GameObject cornerInternalRightDown = null;

    /// <summary>The corner internal right up</summary>
    [SerializeField]
    private GameObject cornerInternalRightUp = null;

    /// <summary>The decoration</summary>
    [SerializeField]
    private List<DecoMenu> decorations = new List<DecoMenu>();

    #region Encapsulate Fields

    /// <summary>Gets or sets the name style.</summary>
    /// <value>The name style.</value>
    public string NameStyle { get => nameStyle; set => nameStyle = value; }

    /// <summary>Gets or sets the floor.</summary>
    /// <value>The floor.</value>
    public GameObject Floor { get => floor; set => floor = value; }

    /// <summary>Gets or sets the wall down.</summary>
    /// <value>The wall down.</value>
    public GameObject WallDown { get => wallDown; set => wallDown = value; }

    /// <summary>Gets or sets the wall left.</summary>
    /// <value>The wall left.</value>
    public GameObject WallLeft { get => wallLeft; set => wallLeft = value; }

    /// <summary>Gets or sets the wall right.</summary>
    /// <value>The wall right.</value>
    public GameObject WallRight { get => wallRight; set => wallRight = value; }

    /// <summary>Gets or sets the wall top.</summary>
    /// <value>The wall top.</value>
    public GameObject WallTop { get => wallTop; set => wallTop = value; }

    /// <summary>Gets or sets the corner left down.</summary>
    /// <value>The corner left down.</value>
    public GameObject CornerLeftDown { get => cornerLeftDown; set => cornerLeftDown = value; }

    /// <summary>Gets or sets the corner right down.</summary>
    /// <value>The corner right down.</value>
    public GameObject CornerRightDown { get => cornerRightDown; set => cornerRightDown = value; }

    /// <summary>Gets or sets the corner left up.</summary>
    /// <value>The corner left up.</value>
    public GameObject CornerLeftUp { get => cornerLeftUp; set => cornerLeftUp = value; }

    /// <summary>Gets or sets the corner right up.</summary>
    /// <value>The corner right up.</value>
    public GameObject CornerRightUp { get => cornerRightUp; set => cornerRightUp = value; }

    /// <summary>Gets or sets the corner internal left down.</summary>
    /// <value>The corner internal left down.</value>
    public GameObject CornerInternalLeftDown { get => cornerInternalLeftDown; set => cornerInternalLeftDown = value; }

    /// <summary>Gets or sets the corner internal left up.</summary>
    /// <value>The corner internal left up.</value>
    public GameObject CornerInternalLeftUp { get => cornerInternalLeftUp; set => cornerInternalLeftUp = value; }

    /// <summary>Gets or sets the corner internal right down.</summary>
    /// <value>The corner internal right down.</value>
    public GameObject CornerInternalRightDown { get => cornerInternalRightDown; set => cornerInternalRightDown = value; }

    /// <summary>Gets or sets the corner internal right up.</summary>
    /// <value>The corner internal right up.</value>
    public GameObject CornerInternalRightUp { get => cornerInternalRightUp; set => cornerInternalRightUp = value; }
    
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
            (tileBox.Equals(BoardBox.WallDown)) ? WallDown :
            (tileBox.Equals(BoardBox.WallLeft)) ? WallLeft :
            (tileBox.Equals(BoardBox.WallRight)) ? WallRight :
            (tileBox.Equals(BoardBox.WallTop)) ? WallTop :
            (tileBox.Equals(BoardBox.CornerLeftUp)) ? CornerLeftUp :
            (tileBox.Equals(BoardBox.CornerRightUp)) ? CornerRightUp :
            (tileBox.Equals(BoardBox.CornerLeftDown)) ? CornerLeftDown :
            (tileBox.Equals(BoardBox.CornerRightDown)) ? CornerRightDown :
            (tileBox.Equals(BoardBox.CornerInternalLeftDown)) ? CornerInternalLeftDown :
            (tileBox.Equals(BoardBox.CornerInternalLeftUp)) ? CornerInternalLeftUp :
            (tileBox.Equals(BoardBox.CornerInternalRightDown)) ? CornerInternalRightDown :
            (tileBox.Equals(BoardBox.CornerInternalRightUp)) ? CornerInternalRightUp :
            Floor;
    }

    #region Validate Fields
#if UNITY_EDITOR
    /// <summary>Called when [validate].</summary>
    public void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, NameStyle);
            AssetDatabase.SaveAssets();

            floor = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/floor.prefab", typeof(GameObject));

            wallDown = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/WallDown.prefab", typeof(GameObject));
            wallLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/WallLeft.prefab", typeof(GameObject));
            WallRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/WallRigth.prefab", typeof(GameObject));
            WallTop = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/WallTop.prefab", typeof(GameObject));

            cornerLeftDown = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerLD.prefab", typeof(GameObject));
            cornerLeftUp = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerLU.prefab", typeof(GameObject));
            cornerRightDown = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerRD.prefab", typeof(GameObject));
            cornerRightUp = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerRU.prefab", typeof(GameObject));

            cornerInternalLeftDown = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerILD.prefab", typeof(GameObject));
            cornerInternalLeftUp = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerILU.prefab", typeof(GameObject));
            cornerInternalRightDown = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerIRD.prefab", typeof(GameObject));
            cornerInternalRightUp = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dungeon/" + NameStyle + "/Board/CornerIRU.prefab", typeof(GameObject));

            DecoMenu[] decocor = new DecoMenu[Decorations.Count];
            Decorations.CopyTo(decocor);
            List<DecoMenu> decoMenuTempList = decocor.ToList();
            Decorations.Clear();

            string path = Application.dataPath + "/Prefabs/Dungeon/"+ NameStyle +"/Decoration";
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
        };
    }
#endif
    #endregion
}