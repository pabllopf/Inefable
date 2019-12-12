//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Clouds.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

/// <summary>Generator of clouds.</summary>
public class Clouds : MonoBehaviour
{
    /// <summary>The maximum clouds</summary>
    [SerializeField]
    private int maxClouds = 100;

    /// <summary>The time to change direction</summary>
    [SerializeField]
    private float timeToChangeDir = 0;

    /// <summary>The time reset</summary>
    private float timeReset = 0;

    /// <summary>The current clouds</summary>
    private int currentClouds = 0;

    /// <summary>The clouds</summary>
    [SerializeField]
    private List<GameObject> clouds = null;

    /// <summary>The x position</summary>
    [SerializeField]
    private Vector2 xRange = Vector2.zero;

    /// <summary>The y range</summary>
    [SerializeField]
    private Vector2 yRange = Vector2.zero;

    /// <summary>The groups</summary>
    [SerializeField]
    private List<GameObject> groups = null;

    /// <summary>The current group</summary>
    private GameObject currentGroup = null;

    /// <summary>The current group position</summary>
    private Vector3 currentGroupPos = Vector3.zero;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.timeReset = this.timeToChangeDir;
        this.CreateClouds();
        this.Move();
    }

    /// <summary>Creates the clouds.</summary>
    private void CreateClouds() 
    {
        if (this.clouds.Count > 0)
        {
            this.CreateGroups();
            while (this.currentClouds <= this.maxClouds) 
            {
                int cloudNum = Random.Range(0, this.clouds.Count);
                GameObject cloudSpawned = MonoBehaviour.Instantiate(this.clouds[cloudNum], new Vector3(Random.Range(this.xRange.x, this.xRange.y), Random.Range(this.yRange.x, this.yRange.y), 0), Quaternion.identity);
                cloudSpawned.transform.parent = this.groups[Random.Range(0, this.groups.Count)].transform;
                int size = Random.Range(4, 11);
                cloudSpawned.transform.localScale = new Vector3(size, size, 0);
                cloudSpawned.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(-3, -1);
                
                this.currentClouds++;
            }
        }
        else 
        {
            Debug.Log("Please insert clouds prefabs to spawn.");
        }
    }

    /// <summary>Creates the groups.</summary>
    private void CreateGroups() 
    {
        this.groups = new List<GameObject>();
        for (int i = 0; i < this.maxClouds / 20; i++) 
        {
            GameObject group = new GameObject();
            group.transform.parent = this.transform;
            group.name = "Group" + (i + 1);
            this.groups.Add(group);
        }
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (this.timeToChangeDir <= 0)
        {
            this.timeToChangeDir = this.timeReset;
            this.Move();
        }
        else 
        {
            this.timeToChangeDir -= Time.deltaTime;
        }

        this.currentGroup.transform.position = Vector3.LerpUnclamped(this.currentGroup.transform.position, this.currentGroupPos, 0.05f * Time.deltaTime);
    }

    /// <summary>Moves this instance.</summary>
    private void Move()
    {
        this.currentGroup = this.groups[Random.Range(0, this.groups.Count)];
        this.currentGroupPos = this.currentGroup.transform.position + new Vector3(Random.Range(-10, 10), 0, 0);
    }
}
