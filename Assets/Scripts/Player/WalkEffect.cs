//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="WalkEffect.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>Manage the effect when walk the player.</summary>
public class WalkEffect : MonoBehaviour
{
    /// <summary>The frequency</summary>
    [Range(0, 10)]
    [SerializeField]
    private float frequency = 1f;

    /// <summary>The particle</summary>
    [SerializeField]
    private GameObject particle = null;

    /// <summary>The map</summary>
    private Tilemap map = null;

    /// <summary>The activated</summary>
    private bool activated = false;

    /// <summary>Stops all.</summary>
    public void StopAll()
    {
        if (this.activated && this.map != null) 
        {
            this.activated = false;
            this.StopAllCoroutines();
        }
    }

    /// <summary>Starts all.</summary>
    public void StartAll()
    {
        if (!this.activated && this.map != null)
        {
            this.activated = true;
            this.StartCoroutine(this.Effect(this.particle, this.frequency));
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        if (GameObject.FindWithTag("TileMap") != null) 
        {
            this.map = GameObject.FindWithTag("TileMap").GetComponent<Tilemap>();
        }
    }

    /// <summary>Effects the specified particle.</summary>
    /// <param name="particle">The particle.</param>
    /// <param name="frequency">The frequency.</param>
    /// <returns>Return none</returns>
    private IEnumerator Effect(GameObject particle, float frequency) 
    {
        particle.GetComponent<SpriteRenderer>().color = this.ColorOfTile();

        MonoBehaviour.Instantiate(particle, this.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(frequency);
       
        this.StartCoroutine(this.Effect(particle, frequency));
    }

    /// <summary>Colors the of tile.</summary>
    /// <returns>Return color </returns>
    private Color ColorOfTile() 
    {
        string tileName = this.map.GetTile(new Vector3Int((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z)).name;
        if (tileName.Contains("Grass")) 
        {
            return new Color(0.2829765f, 0.3773585f, 0.1441794f, 1f);
        }

        if (tileName.Contains("Cobble")) 
        {
            return new Color(0.5566038f, 0.4793922f, 0.3281862f, 1f);
        }
        
        return new Color(0.2735849f, 0.2735849f, 0.2735849f, 0.454902f);
    }
}