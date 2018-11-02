using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour {
    public bool pickable = true;
    public GameObject toPick;
    public string tag;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual GameObject pick()
    {
        toPick.GetComponent<Rigidbody>().isKinematic = true;
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }
        toPick.GetComponent<PickableItem>().enabled = false;
        return toPick;
    }
}
