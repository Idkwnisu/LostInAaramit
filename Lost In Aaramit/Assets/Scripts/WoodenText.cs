using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodenText : MonoBehaviour {

    public Text EndText;

	void Start () {
        EndText.enabled = false;
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndText.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndText.enabled = false;
        }
    }
}
