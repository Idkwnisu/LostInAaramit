using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

    public bool change = true;

    public GameObject FreeCamera;
    public GameObject StaticCamera;
    public GameObject Player;

    public static ControlManager instance = null;

    public Camera currentCamera;

    enum ControlType{FreeCamera, StaticCamera, Joypad};

    public int currentControls = 0;

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

    }


    // Use this for initialization
    void Start () {
        currentCamera = FreeCamera.GetComponent<Camera>();
        if (change)
        {
            if (!PlayerPrefs.HasKey("Controls"))
            {
                PlayerPrefs.SetInt("Controls", currentControls);
            }
            else
            {
                ChangeControlType(PlayerPrefs.GetInt("Controls"));
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (change)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentControls = (currentControls + 1) % 3;
                ChangeControlType(currentControls);
                Debug.Log("Controls changed");
                Debug.Log("Current controls: " + currentControls);
            }
        }
	}

    public Camera getCurrentCamera()
    {
        return currentCamera;
    }

    public void ChangeControlType(int type)
    {
        if (change)
        {
            currentControls = type;
            switch (type)
            {
                case 0:
                    Player.GetComponent<PlayerControllerRun>().enabled = true;
                    Player.GetComponent<PlayerControllerRunNoFreeCamera>().enabled = false;
                    Player.GetComponent<PlayerControllerRunJoypad>().enabled = false;
                    FreeCamera.GetComponent<Camera>().enabled = true;
                    FreeCamera.GetComponent<PlayerFollow>().cameraActive = true;
                    StaticCamera.GetComponent<Camera>().enabled = false;
                    StaticCamera.GetComponent<AudioListener>().enabled = false;
                    FreeCamera.GetComponent<AudioListener>().enabled = true;

                    currentCamera = FreeCamera.GetComponent<Camera>();
                    break;
                case 1:
                    Player.GetComponent<PlayerControllerRun>().enabled = false;
                    Player.GetComponent<PlayerControllerRunNoFreeCamera>().enabled = true;
                    Player.GetComponent<PlayerControllerRunJoypad>().enabled = false;
                    FreeCamera.GetComponent<Camera>().enabled = false;
                    FreeCamera.GetComponent<PlayerFollow>().cameraActive = false;
                    StaticCamera.GetComponent<Camera>().enabled = true;
                    StaticCamera.GetComponent<AudioListener>().enabled = true;
                    FreeCamera.GetComponent<AudioListener>().enabled = false;

                    currentCamera = StaticCamera.GetComponent<Camera>();
                    break;
                case 2:
                    Player.GetComponent<PlayerControllerRun>().enabled = false;
                    Player.GetComponent<PlayerControllerRunNoFreeCamera>().enabled = false;
                    Player.GetComponent<PlayerControllerRunJoypad>().enabled = true;
                    FreeCamera.GetComponent<Camera>().enabled = false;
                    FreeCamera.GetComponent<PlayerFollow>().cameraActive = false;
                    StaticCamera.GetComponent<Camera>().enabled = true;
                    StaticCamera.GetComponent<AudioListener>().enabled = true;
                    FreeCamera.GetComponent<AudioListener>().enabled = false;
                    currentCamera = StaticCamera.GetComponent<Camera>();

                    break;
            }
            PlayerPrefs.SetInt("Controls", currentControls);
        }

    }

}
