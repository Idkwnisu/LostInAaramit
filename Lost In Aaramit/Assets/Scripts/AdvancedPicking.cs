using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPicking : PickableItem {
    public GameObject[] ToActivate;
    public GameObject[] ToDeactivate;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override GameObject pick()
    {
        for(int i = 0; i < ToActivate.Length; i++)
        {
            ToActivate[i].SetActive(true);
        }
        for (int i = 0; i < ToDeactivate.Length; i++)
        {
            ToDeactivate[i].SetActive(false);
        }
        return toPick;
    }
}
