using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorRespawn : MonoBehaviour
{
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            PlayerMovement.Respawn();
        }
     
    }

}
