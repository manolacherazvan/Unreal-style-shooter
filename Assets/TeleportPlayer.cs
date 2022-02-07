using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject[] spawnPoints;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");

        if (collision.gameObject == player)
        {
            PlayerMovement.Respawn();
        }
    }
}
