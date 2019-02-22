using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAtNabla : MonoBehaviour
{
    public GameObject dialogue;

    public bool onlyOnce;

    private bool talkable = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("bossDown") == 1)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            } 
            else
            {            
                if (talkable)
                {
                    dialogue.GetComponent<NPC>().triggerDialogue();
                    if (onlyOnce)
                    {
                        talkable = false;
                    }
                }
            }
        }
    }
}
