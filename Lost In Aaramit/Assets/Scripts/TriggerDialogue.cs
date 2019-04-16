using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {

    public GameObject dialogue;

    public bool onlyOnce;

    private bool talkable = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerPrefs.GetString(dialogue.name + "dialogue").Equals("1"))
        {
            if (talkable)
            {
                dialogue.GetComponent<NPC>().triggerDialogue();

                PlayerPrefs.SetString(dialogue.name + "dialogue", "1");
                if(onlyOnce){
                    talkable = false;
                }
            }
        }
    }
}
