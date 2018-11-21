using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour {

    public Transform ChatBackGround;
    public Transform NPCCharacter;

    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.5f);
    }
    
    void Update () {
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.5f);
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
        if ((other.CompareTag("Player")) && Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<DialogueSystem>().NPCName(false);
        }
    }

    public void triggerDialogue(){
        dialogueSystem.Names = Name;
        dialogueSystem.dialogueLines = sentences;
        FindObjectOfType<DialogueSystem>().NPCName(true);
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}

