using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingFox : MonoBehaviour {

    private DialogueSystem dialogueSystem;
    private bool talking = false;
    public float delay = 2.0f;
    private int currentSentence = 0;
    string[] sentences;

    // Use this for initialization
    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();    
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            talking = true;
            sentences = new string[3];
            sentences[0] = "Hai premuto R";
            sentences[1] = "Non hai premuto T";
            sentences[2] = "Hai proprio premuto R";

            StartDialogue();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            talking = true;
            sentences = new string[3];
            sentences[0] = "Hai premuto T";
            sentences[1] = "Non hai premuto R";
            sentences[2] = "Hai proprio premuto T";

            StartDialogue();
        }
    }

    public void GoOn()
    {
        
        currentSentence++;
        dialogueSystem.continueDialogue();
        if (currentSentence > sentences.Length)
        {
            talking = false;
        }
        else
        {
            Invoke("GoOn", delay);
        }
    }

    public void StartDialogue(string[] sentences)
    {
        setSentences(sentences);
        StartDialogue();
    }

    public void StartDialogue()
    {
        currentSentence = 0;
        GetComponent<NPC>().sentences = sentences;
        GetComponent<NPC>().triggerDialogue();
        Invoke("GoOn", delay);
    }

    public void setSentences(string[] sentences)
    {
        this.sentences = sentences;
    }
}
