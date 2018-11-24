using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public Transform[] target;
    public float speed;

    private int c;

    private void Start() {
        enabled = false;
    }
	private void Update () {
        if(transform.position != target[c].position){
            Vector3 pos = Vector3.MoveTowards(transform.position, target[c].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        } else {
            c = (c + 1) % target.Length;
        }
	}

    public void setActive(){
        enabled = true;
    }

    public void setNotActive()
    {
        enabled = false;
    }
}
