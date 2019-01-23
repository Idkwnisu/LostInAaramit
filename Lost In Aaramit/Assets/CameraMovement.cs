using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject cam;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.GetComponent<Camera>().enabled = true;
            cam.GetComponent<Animation>().Play();

            PlayerControllerRun playerCR = other.GetComponent<PlayerControllerRun>();
            PlayerControllerRunNoFreeCamera playerCRFC = other.GetComponent<PlayerControllerRunNoFreeCamera>();
            PlayerControllerRunJoypad playerCRC = other.GetComponent<PlayerControllerRunJoypad>();


            if (playerCR.enabled)
            {
                playerCR.ControlDisablingPermanent();
                playerCR.resetSpeed();
            }
            if (playerCRFC.enabled)
            {
                playerCRFC.ControlDisablingPermanent();
                playerCRFC.resetSpeed();
            }
            if (playerCRC.enabled)
            {
                playerCRC.ControlDisablingPermanent();
                playerCRC.resetSpeed();
            }
        }

    }
}
