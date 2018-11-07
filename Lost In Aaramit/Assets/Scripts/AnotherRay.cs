using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherRay : LightTouch {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void lightHitted()
    {
        GetComponent<LightRayCaster>().Activate();
    }

    public override void lightGone()
    {
        GetComponent<LightRayCaster>().Deactivate();
    }

}
