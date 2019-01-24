using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guards : MonoBehaviour {

    public GameObject dialoguePlume;

    public GameObject dialogueNoPlume;

    public GameObject Guard1;

    public GameObject Guard2;

    public bool onlyOnce;

    private bool talkable = true;

    public bool havePlume;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (talkable)
            {
                if (PlayerPrefs.GetInt("Plume") == 1)
                {
                    dialoguePlume.GetComponent<NPC>().triggerDialogue();
                    Guard1.GetComponent<PupinsMovement>().setActive();
                    Guard2.GetComponent<PupinsMovement>().setActive();
                } else {
                    dialogueNoPlume.GetComponent<NPC>().triggerDialogue();
                }

            }
        }
    }
}
