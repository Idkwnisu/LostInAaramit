using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFall : MonoBehaviour {

    public float speed;
    public GameObject player;

    private int c;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            player.GetComponent<Rigidbody>().AddForce(Vector3.up*speed, ForceMode.VelocityChange);
        }
    }

}