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

    /// <summary>The time to change dir</summary>
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

    /// <summary>The object</summary>
    private GameObject obj;

    /// <summary>The object 1</summary>
    private GameObject obj1;

    /// <summary>The position</summary>
    private Vector2 pos = Vector2.zero;

    /// <summary>The position</summary>
    private Vector2 pos1 = Vector2.zero;

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
            obj = new GameObject();
            obj.transform.parent = this.transform;
            obj.name = "Group1";
            while (this.currentClouds <= this.maxClouds / 2) 
            {
                int cloudNum = Random.Range(0, this.clouds.Count);
                GameObject cloudSpawned = MonoBehaviour.Instantiate(this.clouds[cloudNum], new Vector3(Random.Range(this.xRange.x, this.xRange.y), Random.Range(this.yRange.x, this.yRange.y), 0), Quaternion.identity);
                cloudSpawned.transform.parent = obj.transform;
                int size = Random.Range(4, 11);
                cloudSpawned.transform.localScale = new Vector3(size, size, 0);
                cloudSpawned.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(-3, -1);
                this.currentClouds++;
            }

            obj1 = new GameObject();
            obj1.transform.parent = this.transform;
            obj1.name = "Group2";
            while (this.currentClouds <= this.maxClouds)
            {
                int cloudNum = Random.Range(0, this.clouds.Count);
                GameObject cloudSpawned = MonoBehaviour.Instantiate(this.clouds[cloudNum], new Vector3(Random.Range(this.xRange.x, this.xRange.y), Random.Range(this.yRange.x, this.yRange.y), 0), Quaternion.identity);
                cloudSpawned.transform.parent = obj1.transform;
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

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (timeToChangeDir <= 0)
        {
            timeToChangeDir = timeReset;
            this.Move();
        }
        else 
        {
            timeToChangeDir -= Time.deltaTime;
        }

        this.obj.transform.position = Vector3.LerpUnclamped(this.obj.transform.position, this.pos, 0.05f * Time.deltaTime);
        this.obj1.transform.position = Vector3.LerpUnclamped(this.obj1.transform.position, this.pos1, 0.05f * Time.deltaTime);
    }

    /// <summary>Moves this instance.</summary>
    private void Move()
    {
        this.pos = this.obj.transform.position + new Vector3(Random.Range(-10, 10), 0, 0);
        this.pos1 = this.obj1.transform.position + new Vector3(Random.Range(-10, 10), 0, 0);
    }

}
