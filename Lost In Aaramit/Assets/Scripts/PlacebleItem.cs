using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacebleItem : MonoBehaviour {
   
    public Transform toPlace;
    public string tag;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Place(GameObject toPlace)
    {
        toPlace.transform.position = toPlace.transform.position;
        toPlace.transform.rotation = toPlace.transform.rotation;
    }
}
