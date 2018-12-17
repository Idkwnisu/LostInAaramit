using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnRemoteObject : MonoBehaviour {
    public Text InteractText;
    public GameObject rotatorLayout;
    public Camera mainCamera;
    public Camera objectCamera;
    public GameObject Player;

    public GameObject indicator;
    public GameObject toRotate;

    public float rotationSpeed;
    public float maxRotationSpeed;

    [Range(0.01f, 1)]
    public float Smoothness;

    private float currentRotationSpeedX;
    private float currentRotationSpeedY;


    private bool interactable = false;
    private bool interacting = false;
	// Use this for initialization
	void Start () {
        InteractText.enabled = false;
        objectCamera.enabled = false;
        rotatorLayout.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(interactable)
        {
            if(Input.GetButtonDown("Interact"))
            {
                if (interacting)
                {
                    interacting = false;
                    mainCamera.enabled = true;
                    objectCamera.enabled = false;
                    InteractText.enabled = true;
                    rotatorLayout.SetActive(false);
                    if (Player.GetComponent<PlayerControllerRun>() != null)
                    {
                        Player.GetComponent<PlayerControllerRun>().ControlEnabling();
                    }
                    else
                    {
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().ControlEnabling();
                    }

                }
                else
                {
                    interacting = true;
                    mainCamera.enabled = false;
                    objectCamera.enabled = true;
                    InteractText.enabled = false;
                    rotatorLayout.SetActive(true);
                    if (Player.GetComponent<PlayerControllerRun>() != null)
                    {
                        Player.GetComponent<PlayerControllerRun>().resetSpeed();
                        Player.GetComponent<PlayerControllerRun>().ControlDisabling();
                    }
                    else
                    {
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().resetSpeed();
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().ControlDisabling();
                    }
                }
            }
            if (interacting)
            {
                float h = Input.GetAxis("Horizontal");
                if (h == 0)
                {
                    currentRotationSpeedY =  Mathf.Lerp(currentRotationSpeedY, 0, Smoothness);

                }
                else
                {
                    currentRotationSpeedY = Mathf.Clamp(currentRotationSpeedY + h * rotationSpeed, (-1) * maxRotationSpeed, maxRotationSpeed);
                }

                float v = Input.GetAxis("Vertical");
                if (v == 0)
                {
                    currentRotationSpeedX = Mathf.Lerp(currentRotationSpeedX, 0, Smoothness);

                }
                else
                {
                    currentRotationSpeedX = Mathf.Clamp(currentRotationSpeedX + v * rotationSpeed, (-1) * maxRotationSpeed, maxRotationSpeed);
                }



            }
            if (currentRotationSpeedY != 0 || currentRotationSpeedX != 0)
            {
                Vector3 vec = new Vector3(0,0,0);
                
                vec.y += currentRotationSpeedY * Time.deltaTime;
                vec.z += currentRotationSpeedX * Time.deltaTime;
                indicator.transform.Rotate(vec,Space.World);
                toRotate.GetComponent<RotateThis>().Rotate(vec);
            }
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
     if(other.CompareTag("Player"))
        {
            InteractText.enabled = true;
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractText.enabled = false;
            interactable = false;
        }
    }
}
