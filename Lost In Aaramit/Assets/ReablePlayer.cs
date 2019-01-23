using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReablePlayer : MonoBehaviour {

    public GameObject player;
    public GameObject kitchi;

	// Use this for initialization
	void Awake () {
		if(PlayerPrefs.HasKey("Saved"))
        {
            reable();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void reable()
    {
        player.active = true;
        kitchi.active = true;
        gameObject.active = false;
    }
}
