using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTown : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.GetComponent<PauseMenu>().GoToTown();
        }
    }
}
