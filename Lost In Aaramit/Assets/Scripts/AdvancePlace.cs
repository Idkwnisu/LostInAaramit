using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancePlace : PlacebleItem {

    public GameObject[] ToActivate;
    public GameObject[] ToDeactivate;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Place(GameObject objectToPlace)
    {
        for (int i = 0; i < ToActivate.Length; i++)
        {
            ToActivate[i].SetActive(true);
        }
        for (int i = 0; i < ToDeactivate.Length; i++)
        {
            ToDeactivate[i].SetActive(false);
        }
        objectToPlace.transform.SetParent(transform);
        objectToPlace.transform.position = toPlace.transform.position;
        objectToPlace.transform.rotation = toPlace.transform.rotation;
    }
}
