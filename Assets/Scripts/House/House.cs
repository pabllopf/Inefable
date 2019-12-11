using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class House : MonoBehaviour
{
    /*
    private string houseMatrix;
    private int[,] house;
    private Tilemap tilemap;

    public GameObject player;
    private GameObject currentPlayer;

    public GameObject editCamera;
    private GameObject currentCamera;

    public GameObject uiEditMode;
    private GameObject currentUI;

    public TileBase selector;
    private Vector3Int selectorPos;

    public TileBase background;
    public TileBase floor;

    private bool editMode;
    private bool tileSelected;
    


    private void Start()
    {
        InitParametres();
        LoadHouse();
        SpawnPlayer();
    }

    private void InitParametres()
    {
        Data.Load();
        houseMatrix = Global.current.houseMatrix;
        house = new int[50, 50];
        tilemap = transform.Find("Tilemap").GetComponent<Tilemap>();
        editMode = false;
        tileSelected = false;
        selectorPos = new Vector3Int(0, 0, 1);
    }

    private void LoadHouse()
    {
        string[] sentences = houseMatrix.Split('/');
        for (int i = 0; i < sentences.Length - 1; i++)
        {
            string[] currentsentence = sentences[i].Split('-');
            for (int j = 0; j < currentsentence.Length - 1; j++)
            {
                house[i, j] = Int32.Parse(currentsentence[j]);
            }
        }

        for (int i = 0; i < house.GetLength(0); i++)
        {
            for (int j = 0; j < house.GetLength(1); j++)
            {
                int x = -(house.GetLength(0) / 2) + i;
                int y = -(house.GetLength(1) / 2) + j;

                if (house[i, j] == 0) { tilemap.SetTile(new Vector3Int(x, y, 0), background); }
                if (house[i, j] == 1) { tilemap.SetTile(new Vector3Int(x, y, 0), floor); }
            }
        }
    }

    private void SaveMap()
    {
        string save = "";
        for (int i = 0; i < house.GetLength(0); i++)
        {
            string sentence = "";
            for (int j = 0; j < house.GetLength(1); j++)
            {
                sentence += house[i, j] + "-";

            }
            save += sentence + "/";
        }
        Global.current.houseMatrix = save;
        Data.Save();
    }


    private void SpawnPlayer()
    {
        GameObject playerSpawned = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        currentPlayer = playerSpawned;
    }

    private void SpawnEditMode()
    {
        GameObject cam = Instantiate(editCamera, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        currentCamera = cam;

        GameObject ui = Instantiate(uiEditMode, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        currentUI = ui;
    }


    private void Update()
    {
        if (!Input.anyKey) { return; }
        if (Input.GetKeyDown(KeyCode.M)) { EditMode(); }
        if (editMode) { EditModeOn(); }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { if (tileSelected) { tileSelected = false; } else { tileSelected = true;} }
        if (tileSelected && Input.GetKeyDown(KeyCode.E)) { DrawTile(floor,1); };
        if (tileSelected && Input.GetKeyDown(KeyCode.Q)) { DeleteTile(); };
    }


    private void EditMode() 
    {
        if (editMode)
        {
            editMode = false;
            Selector(false);
            SaveMap();
            SpawnPlayer();
            Destroy(currentCamera);
            Destroy(currentUI);
            return;
        }
        else 
        {
            editMode = true;
            Selector(true);
            SpawnEditMode();
            Destroy(currentPlayer);
            return;
        }
    }

    private void Selector(bool state) 
    {
        if (state == true) { tilemap.SetTile(selectorPos, selector); }
        else { tilemap.SetTile(selectorPos, null); }
    }


    private void EditModeOn()
    {
        if (Input.GetKeyDown(KeyCode.W)) { MoveSelector(new Vector3Int(0, 1, 0)); return; }
        if (Input.GetKeyDown(KeyCode.S)) { MoveSelector(new Vector3Int(0,-1, 0)); return; }
        if (Input.GetKeyDown(KeyCode.D)) { MoveSelector(new Vector3Int( 1,0, 0)); return; }
        if (Input.GetKeyDown(KeyCode.A)) { MoveSelector(new Vector3Int(-1,0, 0)); return; }
    }

    private void MoveSelector(Vector3Int newPosition) 
    {
        tilemap.SetTile(selectorPos, tilemap.GetTile(selectorPos - new Vector3Int(0, 0, -1)));
        selectorPos += newPosition;
        currentCamera.transform.position = selectorPos + new Vector3(0.5f, 0.5f, -1);
        tilemap.SetTile(selectorPos, selector);
    }

    private void DrawTile(TileBase tile, int position) 
    {
        int x = house.GetLength(0) / 2 + selectorPos.x;
        int y = house.GetLength(1) / 2 + selectorPos.y;
        house[x, y] = position;
        tilemap.SetTile(selectorPos - new Vector3Int(0,0,-1), tile);
    }

    private void DeleteTile() 
    {
        int x = house.GetLength(0) / 2 + selectorPos.x;
        int y = house.GetLength(1) / 2 + selectorPos.y;
        house[x, y] = 0;
        tilemap.SetTile(selectorPos - new Vector3Int(0, 0, -1), background);
    }
    */
}
