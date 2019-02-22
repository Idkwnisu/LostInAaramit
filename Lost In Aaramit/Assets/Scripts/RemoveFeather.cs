using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFeather : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        if (PlayerPrefs.GetInt("Plume") == 1)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
