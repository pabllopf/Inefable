using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DecoMenu
{
    private Decoration decoration = null;

    /// <summary>The decoration</summary>
    [SerializeField]
    private GameObject prefab = null;

    /// <summary>The box to spawn</summary>
    [SerializeField]
    private BoardBox boxToSpawn = BoardBox.Floor;

    /// <summary>The minimum to spawn</summary>
    [SerializeField]
    [Range(0, 100)]
    private int minToSpawn = 0;

    /// <summary>The maximum to spawn</summary>
    [SerializeField]
    [Range(0, 100)]
    private int maxToSpawn = 0;

    public Decoration Decoration { get => decoration; set => decoration = value; }

    public GameObject Prefab { get => prefab; set => prefab = value; }

    public BoardBox BoxToSpawn { get => boxToSpawn; set => boxToSpawn = value; }

    public int MinToSpawn { get => minToSpawn; set => minToSpawn = value; }
    
    public int MaxToSpawn { get => maxToSpawn; set => maxToSpawn = value; }
    
}
