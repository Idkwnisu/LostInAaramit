using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableItem : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = true;
         }
        GetComponent<PickableItem>().enabled = true;
    }
}
