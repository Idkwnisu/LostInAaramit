using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

    public GameObject FreeCamera;
    public GameObject StaticCamera;
    public GameObject Player;

    private ControlManager instance;

    enum ControlType{FreeCamera, StaticCamera, Joypad};

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeControlType(int type)
    {
        switch(type)
        {
            case 0:
                Player.GetComponent<PlayerControllerRun>().enabled = true;
                Player.GetComponent<PlayerControllerRunNoFreeCamera>().enabled = false;
                Player.GetComponent<PlayerControllerRunJoypad>().enabled = false;
                FreeCamera.GetComponent<Camera>().enabled = true;
                FreeCamera.GetComponent<PlayerFollow>().cameraActive = true;
                StaticCamera.GetComponent<Camera>().enabled = false;
                break;
            case 1:
                Player.GetComponent<PlayerControllerRun>().enabled = false;
                Player.GetComponent<PlayerControllerRunNoFreeCamera>().enabled = true;
                Player.GetComponent<PlayerControllerRunJoypad>().enabled = false;
                FreeCamera.GetComponent<Camera>().enabled = false;
                FreeCamera.GetComponent<PlayerFollow>().cameraActive = false;
                StaticCamera.GetComponent<Camera>().enabled = true;
                break;
            case 2:
                Player.GetComponent<PlayerControllerRun>().enabled = false;
                Player.GetComponent<PlayerControllerRunNoFreeCamera>().enabled = false;
                Player.GetComponent<PlayerControllerRunJoypad>().enabled = true;
                FreeCamera.GetComponent<Camera>().enabled = false;
                FreeCamera.GetComponent<PlayerFollow>().cameraActive = false;
                StaticCamera.GetComponent<Camera>().enabled = true;
                break;
        }
    }

    
}
