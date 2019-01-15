using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFall : MonoBehaviour {

    public float speed;
    public GameObject player;

    private int c;

    public int target = 80;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }

    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            //player.GetComponent<PlayerControllerRun>().ControlDisabling();
            player.GetComponent<Rigidbody>().AddForce(Vector3.up*speed*Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player.GetComponent<PlayerControllerRun>().ControlDisabling();

            PlayerControllerRun playerCR = other.GetComponent<PlayerControllerRun>();
            PlayerControllerRunNoFreeCamera playerCRFC = other.GetComponent<PlayerControllerRunNoFreeCamera>();
            PlayerControllerRunJoypad playerCRC = other.GetComponent<PlayerControllerRunJoypad>();


            if (playerCR.enabled)
            {
                playerCR.ControlDisabling();
                playerCR.resetSpeed();
            }
            if (playerCRFC.enabled)
            {
                playerCRFC.ControlDisabling();
                playerCRFC.resetSpeed();
            }
            if (playerCRC.enabled)
            {
                playerCRC.ControlDisabling();
                playerCRC.resetSpeed();
            }
        }
    }
 
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerControllerRun>().ControlEnabling();

        }
    }
    */
}