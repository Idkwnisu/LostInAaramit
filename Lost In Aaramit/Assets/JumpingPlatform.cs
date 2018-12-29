using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour {
    public Transform target;
    public float VerticalSpeed;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        transform.hasChanged = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.hasChanged)
        {
            transform.parent.LookAt(target);
            transform.hasChanged = false;
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerControllerRun playerCR = other.GetComponent<PlayerControllerRun>();
            PlayerControllerRunNoFreeCamera playerCRFC = other.GetComponent<PlayerControllerRunNoFreeCamera>();
            PlayerControllerRunJoypad playerCRC = other.GetComponent<PlayerControllerRunJoypad>();


            if (playerCR.enabled)
            {
                playerCR.ControlDisabling();
                playerCR.resetSpeed();
                playerCR.applyForce(target.position - transform.position, VerticalSpeed);
            }
            if (playerCRFC.enabled)
            {
                playerCRFC.ControlDisabling();
                playerCRFC.resetSpeed();
                playerCRFC.applyForce(target.position - transform.position, VerticalSpeed);

            }
            if (playerCRC.enabled)
            {
                playerCRC.ControlDisabling();
                playerCRC.resetSpeed();
                playerCRC.applyForce(target.position - transform.position, VerticalSpeed);

            }
        }
    }
}
