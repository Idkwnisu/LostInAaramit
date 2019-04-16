using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumeCheckGuard : MonoBehaviour {
    public Transform plumePosition;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Plume") == 1)
        {
            Debug.Log(PlayerPrefs.GetInt("Plume"));
            transform.position = plumePosition.position;
            GetComponent<BoxCollider>().enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
