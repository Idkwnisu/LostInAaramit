using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThis : MonoBehaviour {

    public bool keepObjectsRotation = true;
    public Transform point;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rotate(Vector3 rotation)
    {
        Vector3 counterRotation = rotation * (-1);
        if(point == null)
            transform.Rotate(rotation, Space.World);
        else
        {
            transform.RotateAround(point.position,Vector3.right,rotation.x);
            transform.RotateAround(point.position, Vector3.up, rotation.y);
            transform.RotateAround(point.position, Vector3.forward, rotation.z);
        }
        if(keepObjectsRotation)
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Rotate(counterRotation,Space.World);
        }
    }
}
