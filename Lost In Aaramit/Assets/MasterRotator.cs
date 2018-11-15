using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterRotator : MonoBehaviour {
    public GameObject[] clockWiseRotation;
    public GameObject[] counterClockWiseRotation;
    public Vector3 rotationSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < clockWiseRotation.Length; i++)
        {
            clockWiseRotation[i].GetComponent<RotateThis>().Rotate(rotationSpeed * Time.deltaTime);
        }
        for (int i = 0; i < counterClockWiseRotation.Length; i++)
        {
            counterClockWiseRotation[i].GetComponent<RotateThis>().Rotate((-1)*rotationSpeed * Time.deltaTime);
        }
    }
}
