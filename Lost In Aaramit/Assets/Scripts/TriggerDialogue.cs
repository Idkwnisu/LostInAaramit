using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {

    public GameObject dialogue;

    public bool onlyOnce;

    private bool talkable = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (talkable)
            {
                dialogue.GetComponent<NPC>().triggerDialogue();
                if(onlyOnce){
                    talkable = false;
                }
            }
        }
    }
}
