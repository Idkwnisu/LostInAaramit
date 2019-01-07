using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleActivator : MonoBehaviour {

    public GameObject Aviator;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Aviator.GetComponent<PlayWhistle>().startPlay();
        }
    }
}
