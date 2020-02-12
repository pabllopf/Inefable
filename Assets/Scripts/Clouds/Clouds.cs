//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Clouds.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Generator of clouds.</summary>
public class Clouds : MonoBehaviour
{
    /// <summary>The maximum clouds</summary>
    [SerializeField]
    private readonly int maxClouds = 100;

    /// <summary>The time to change direction</summary>
    [SerializeField]
    private readonly float timeToChange = 0;

    /// <summary>The current clouds</summary>
    private int currentClouds = 0;

    /// <summary>The clouds</summary>
    [SerializeField]
    private readonly List<GameObject> clouds = null;

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
        CreateClouds();
        StartCoroutine(MoveControl(timeToChange));
    }

    /// <summary>Creates the clouds.</summary>
    private void CreateClouds()
    {
        CreateGroups();
        while (currentClouds <= maxClouds)
        {
            int cloudNum = Random.Range(0, clouds.Count);
            GameObject cloudSpawned = MonoBehaviour.Instantiate(clouds[cloudNum], new Vector3(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y), 0), Quaternion.identity);
            cloudSpawned.transform.parent = groups[Random.Range(0, groups.Count)].transform;
            int size = Random.Range(4, 11);
            cloudSpawned.transform.localScale = new Vector3(size, size, 0);
            cloudSpawned.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(-3, -1);

            currentClouds++;
        }
    }

    /// <summary>Creates the groups.</summary>
    private void CreateGroups()
    {
        groups = new List<GameObject>();
        for (int i = 0; i < maxClouds / 20; i++)
        {
            GameObject group = new GameObject();
            group.transform.parent = transform;
            group.name = "Group" + (i + 1);
            groups.Add(group);
        }
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        currentGroup.transform.position = Vector3.LerpUnclamped(currentGroup.transform.position, currentGroupPos, 0.05f * Time.deltaTime);
    }

    /// <summary>Moves the control.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator MoveControl(float time)
    {
        currentGroup = groups[Random.Range(0, groups.Count)];
        currentGroupPos = currentGroup.transform.position + new Vector3(Random.Range(-10, 10), 0, 0);
        yield return new WaitForSeconds(time);
        StartCoroutine(MoveControl(timeToChange));
    }
}
