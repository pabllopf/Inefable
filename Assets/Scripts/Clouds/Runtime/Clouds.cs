//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Clouds.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace CloudsGenerator
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>Generator of clouds.</summary>
    public class Clouds : MonoBehaviour
    {
        [Header("Configuration:")]

        /// <summary>The maximum number of clouds</summary>
        [SerializeField]
        [Range(1, 250)]
        private int maxNumOfClouds = 100;

        /// <summary>The x position</summary>
        [SerializeField]
        private Vector2 xRange = Vector2.zero;

        /// <summary>The y range</summary>
        [SerializeField]
        private Vector2 yRange = Vector2.zero;

        /// <summary>The clouds prefabs</summary>
        [SerializeField]
        private List<GameObject> cloudsPrefabs = null;

        #region Encapsulate Fields

        /// <summary>Gets or sets the maximum number of clouds.</summary>
        /// <value>The maximum number of clouds.</value>
        public int MaxNumOfClouds { get => maxNumOfClouds; set => maxNumOfClouds = value; }

        /// <summary>Gets or sets the x range.</summary>
        /// <value>The x range.</value>
        public Vector2 XRange { get => xRange; set => xRange = value; }
        
        /// <summary>Gets or sets the y range.</summary>
        /// <value>The y range.</value>
        public Vector2 YRange { get => yRange; set => yRange = value; }

        /// <summary>Gets or sets the clouds prefabs.</summary>
        /// <value>The clouds prefabs.</value>
        public List<GameObject> CloudsPrefabs { get => cloudsPrefabs; set => cloudsPrefabs = value; }
       
        #endregion

        /// <summary>Starts this instance.</summary>
        private void Start()
        {
            if (cloudsPrefabs == null) 
            {
                Debug.LogError("Empty clouds list.");
            }

            while (maxNumOfClouds > 0)
            {
                GameObject cloud = cloudsPrefabs[Random.Range(0, cloudsPrefabs.Count - 1)];
                GameObject cloudSpawned = Instantiate(cloud, new Vector2(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y)), Quaternion.identity, this.transform);

                int size = Random.Range(4, 11);
                cloudSpawned.transform.localScale = new Vector3(size, size, 0);
                cloudSpawned.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(-3, -1);

                maxNumOfClouds--;
            }
        }
    }
}
