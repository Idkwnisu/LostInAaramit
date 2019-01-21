﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Text pressChatText;
   
    public Transform dialogueBoxGUI;
    public Transform ChatBackGround;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public string Names;

    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;

    public AudioClip audioClip;
    AudioSource audioSource;

    private bool automaticContinue = false;

    void Start()
    {
        pressChatText.enabled = false;
        audioSource = GetComponent<AudioSource>();
        dialogueText.text = "";
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.7f);
    }

    void Update()
    {
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.7f);
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        Debug.Log("Start");
        pressChatText.enabled = true;
        if (dialogueActive == true)
        {
            pressChatText.enabled = false;
        }
    }

    public void NPCName(bool trig)
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        //nameText.text = Names;
        if ((Input.GetKeyDown(KeyCode.F) && !trig) || trig)
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetButtonDown("Dialogue") || automaticContinue && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            char endName = ' ';

            nameText.text = "";

            while (endName != '@')
            {
                if (stringToDisplay[currentCharacterIndex] != '@'){
                    nameText.text += stringToDisplay[currentCharacterIndex];
                }
                currentCharacterIndex++;
                endName = stringToDisplay[currentCharacterIndex];
            }

            currentCharacterIndex++;
            currentCharacterIndex++;

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    //Debug.Log(automaticContinue);

                    if (Input.GetButtonDown("Dialogue") || automaticContinue)
                    {
                        automaticContinue = false;

                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetButtonDown("Dialogue") || automaticContinue)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void continueDialogue()
    {
        automaticContinue = true;
    }

    public void DropDialogue()
    {
        Debug.Log("Stop");
        pressChatText.enabled = false;
        dialogueBoxGUI.gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            pressChatText.enabled = false;
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }
}
