using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public Transform[] target;
    private float v = 4f;

    private int c;

    private void Start() {
        enabled = false;
    }
	private void Update () {
        if(transform.position != target[c].position){
            Vector3 pos = Vector3.MoveTowards(transform.position, target[c].position, v * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        } else {
            c = (c + 1) % target.Length;
        }
	}

    public void setActive(float speed){
        enabled = true;
        v = speed;

    }

    public void setNotActive()
    {
        enabled = false;
    }
}
