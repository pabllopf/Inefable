using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Chest : MonoBehaviour
{
    public Sprite openChest;
    private SpriteRenderer chest;
    private BoxCollider2D boxCollider;

    public List<GameObject> items;

    private void Start()
    {
        chest = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OpenChest(Transform player)
    {
        chest.sprite = openChest;
        boxCollider.enabled = false;
        SpawnRamdonObjects(player);
    }

    private void SpawnRamdonObjects(Transform player)
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 pos1 = this.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 pos2 = this.transform.position + new Vector3(0, -0.5f, 0);
        Vector3 pos3 = this.transform.position + new Vector3(0.5f, 0, 0);
        Vector3 pos4 = this.transform.position + new Vector3(-0.5f, 0, 0);

        positions.Add(pos1);
        positions.Add(pos2);
        positions.Add(pos3);
        positions.Add(pos4);

        foreach (GameObject item in items)
        {
            int ramdon = Random.Range(0, 2);
            int counter = 0;
            foreach (Vector3 pos in positions)
            {
                var collisions = Physics2D.OverlapCircleAll(pos, 0.2f);
                if (collisions.Length == 0 && counter < ramdon)
                {
                    Instantiate(item, pos, Quaternion.identity);
                    counter++;
                }
            }
        }
    }
}
