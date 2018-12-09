using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public Transform checkPoint;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            player.transform.position = checkPoint.position;
            player.transform.rotation = checkPoint.rotation;
        }
    }
}
