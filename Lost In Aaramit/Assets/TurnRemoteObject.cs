using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnRemoteObject : MonoBehaviour {
    public Text InteractText;
    public GameObject rotatorLayout;
    public Camera objectCamera;
    public GameObject Player;

    public GameObject indicator;
    public GameObject toRotate;

    public float rotationSpeed;
    public float maxRotationSpeed;

    [Range(0.01f, 1)]
    public float Smoothness;

    public bool Horizontal;
    public bool Vertical;

    private float currentRotationSpeedX;
    private float currentRotationSpeedY;

    private Vector3 lastVel = Vector3.zero;


    private bool interactable = false;
    private bool interacting = false;
	// Use this for initialization
	void Start () {
        InteractText.enabled = false;
        objectCamera.enabled = false;
        rotatorLayout.SetActive(false);
        if(!Horizontal)
        {
            indicator.transform.localScale = new Vector3(1, 1, 0.2f);
        }

        if(!Vertical)
        {
            indicator.transform.localScale = new Vector3(1, 0.2f, 1);
        }
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
                    ControlManager.instance.getCurrentCamera().enabled = true;
                    objectCamera.enabled = false;
                    InteractText.enabled = true;
                    rotatorLayout.SetActive(false);
                    if (Player.GetComponent<PlayerControllerRun>() != null)
                    {
                        Player.GetComponent<PlayerControllerRun>().NonInteracting();
                    }
                    else
                    {
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().NonInteracting();
                    }
                    currentRotationSpeedX = 0;
                    currentRotationSpeedY = 0;
                }
                else
                {
                    interacting = true;
                    ControlManager.instance.getCurrentCamera().enabled = false;
                    objectCamera.enabled = true;
                    InteractText.enabled = false;
                    rotatorLayout.SetActive(true);
                    if (Player.GetComponent<PlayerControllerRun>() != null)
                    {
                        Player.GetComponent<PlayerControllerRun>().resetSpeed();
                        Player.GetComponent<PlayerControllerRun>().Interacting();
                    }
                    else
                    {
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().resetSpeed();
                        Player.GetComponent<PlayerControllerRunNoFreeCamera>().Interacting();
                    }
                }
            }
            if (interacting)
            {
                float h = Input.GetAxis("Horizontal");
                if (h == 0)
                {
                    currentRotationSpeedY =  Mathf.Lerp(currentRotationSpeedY, 0, Smoothness);
                    if (Mathf.Abs(currentRotationSpeedY) < 0.01)
                    {
                        currentRotationSpeedY = 0.0f;
                    }
                    

                }
                else
                {
                    currentRotationSpeedY = Mathf.Clamp(currentRotationSpeedY + h * rotationSpeed, (-1) * maxRotationSpeed, maxRotationSpeed);
                    
                }

                float v = Input.GetAxis("Vertical");
                if (v == 0)
                {
                    currentRotationSpeedX = Mathf.Lerp(currentRotationSpeedX, 0, Smoothness);
                    if (Mathf.Abs(currentRotationSpeedX) < 0.01)
                    {
                        currentRotationSpeedX = 0.0f;
                    }
                }
                else
                {
                    currentRotationSpeedX = Mathf.Clamp(currentRotationSpeedX + v * rotationSpeed, (-1) * maxRotationSpeed, maxRotationSpeed);
                }



            }
            if (currentRotationSpeedY != 0 || currentRotationSpeedX != 0)
            {
                Vector3 vec = new Vector3(0,0,0);

                if (Horizontal && ((lastVel == Vector3.zero) || lastVel.y != 0))
                {
                    vec.y += currentRotationSpeedY * Time.deltaTime;
                }
                if (Vertical && ((lastVel.y == 0 && lastVel.z == 0) || lastVel.z != 0))
                {
                    vec.z += currentRotationSpeedX * Time.deltaTime;
                }
                if(vec.z != 0 && vec.y != 0)
                {
                    vec.z = 0;
                }
                lastVel = vec;
                indicator.transform.Rotate(vec,Space.World);
                toRotate.GetComponent<RotateThis>().Rotate(vec);
            }
            else
            {
                lastVel = Vector3.zero;
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
            currentRotationSpeedX = 0;
            currentRotationSpeedY = 0;
        }
    }
}
