using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour {

    public Transform ChatBackGround;
    public Transform NPCCharacter;
    public bool talkable = true;

    private DialogueSystem dialogueSystem;

    public string Name;

    private DialogueSystem dialogue;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.5f);
    }
    
    void Update () {
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.5f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (talkable)
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.EnterRangeOfNPC();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (talkable)
        {
            if ((other.CompareTag("Player")) && Input.GetKeyDown(KeyCode.F))
            {
                this.gameObject.GetComponent<NPC>().enabled = true;
                dialogueSystem.Names = Name;
                dialogueSystem.dialogueLines = sentences;
                dialogueSystem.NPCName(false);
            }
        }
    }

    public void triggerDialogue(){
        dialogueSystem.Names = Name;
        dialogueSystem.dialogueLines = sentences;
        dialogueSystem.NPCName(true);
    }

    public void OnTriggerExit()
    {
        dialogueSystem.OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}

