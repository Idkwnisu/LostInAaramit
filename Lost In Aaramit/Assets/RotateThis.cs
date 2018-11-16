﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThis : MonoBehaviour {

    public bool keepObjectsRotation = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rotate(Vector3 rotation)
    {
        Vector3 counterRotation = rotation * (-1);
        transform.Rotate(rotation, Space.World);
        if(keepObjectsRotation)
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Rotate(counterRotation);
        }
    }
}
