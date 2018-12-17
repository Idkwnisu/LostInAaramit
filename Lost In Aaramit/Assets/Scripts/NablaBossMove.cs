using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NablaBossMove : MonoBehaviour {

    public Transform[] targetPlayer;
    public Transform[] targetBoss;
    public GameObject NablaAndPlayer;
    public GameObject NablaBoss;
    public GameObject Boss;
    public GameObject Allen;

    public GameObject DieCube;

    public float speed;

    private bool yetEnter;
    private int c = 0;
    private int cb = 0;

    private void Start() {
        yetEnter = false;
        enabled = false;
        DieCube.GetComponent<BoxCollider>().enabled = false;
    }

    private void Update () {
        if(NablaAndPlayer.transform.position != targetPlayer[c].position){
            Vector3 pos = Vector3.MoveTowards(NablaAndPlayer.transform.position, targetPlayer[c].position, speed * Time.deltaTime);
            NablaAndPlayer.GetComponent<Rigidbody>().MovePosition(pos);
        } else {
            if (c == (targetPlayer.Length - 1))
            {
                setNotActive();
            }
            else
            {
                c = (c + 1) % targetPlayer.Length;
            }
        }

        if (NablaBoss.transform.position != targetBoss[cb].position)
        {
            Vector3 pos2 = Vector3.MoveTowards(NablaBoss.transform.position, targetBoss[cb].position, speed * Time.deltaTime);
            Vector3 posB = pos2 + new Vector3(0, 1f, 0);
            NablaBoss.GetComponent<Rigidbody>().MovePosition(pos2);
            Boss.GetComponent<Rigidbody>().MovePosition(posB);
        }
        else
        {
            if (cb != (targetBoss.Length - 1)){
                cb = (cb + 1) % targetBoss.Length;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!yetEnter)
        {
            if (other.CompareTag("Player"))
            {
                setActive();
                yetEnter = true;
            }
        }
    }

    public void setActive(){
        enabled = true;
    }

    public void setNotActive()
    {
        DieCube.GetComponent<BoxCollider>().enabled = true;
        enabled = false;
    }
}
