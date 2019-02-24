using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour {
    public Camera mainCamera;
    public Camera newCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            mainCamera.enabled = false;
            newCamera.enabled = true;
            ControlManager.instance.currentCamera = newCamera;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ControlManager.instance.currentCamera.GetInstanceID() == newCamera.GetInstanceID())
            {
                mainCamera.enabled = true;
                ControlManager.instance.currentCamera = mainCamera;
            }
            newCamera.enabled = false;
        }
    }
}
