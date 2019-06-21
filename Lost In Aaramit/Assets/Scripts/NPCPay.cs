using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPCPay : MonoBehaviour {

    public Transform ChatBackGround;
    public Transform NPCCharacter;
    public bool talkable = true;

    private bool move;

    public GameObject target;

    private DialogueSystem dialogueSystem;

    private DialogueSystem dialogue;

    public bool done = false;

    [TextArea(5, 10)]
    public string[] sentencesNoMoney;

    [TextArea(5, 10)]
    public string[] sentencesMoney;

    [TextArea(5, 10)]
    public string[] sentencesDone;

    public GameObject block;

    public float speed = 10.0f;

    public int payment = 6;

    private bool deactivated = false;

    private bool walking = false;
    private bool walked = false;

    void Start () {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.8f);
        if(PlayerPrefs.GetFloat("PlanetariumDone") == 1)
        {
            block.SetActive(false);
            done = true;
            transform.position = target.transform.position;
            walked = true;
        }
    }
    
    void Update () {
        ChatBackGround.GetComponent<CanvasRenderer>().SetAlpha(0.8f);
        if(dialogueSystem.dialogueEnded)
        {
            deactivated = false;
        }
        if(done && dialogueSystem.dialogueEnded)
        {
            if(!walked)
            {
                walking = true;
            }
        }
        if(walking && !walked)
        {
            GetComponent<Animator>().SetBool("Walking", true);

            if(Vector3.Distance(transform.position, target.transform.position) > 0.01f)
            {
                Vector3 toLook = new Vector3(target.transform.position.x, transform.position.y,target.transform.position.z);
                transform.LookAt(toLook);
                transform.position = transform.position + (target.transform.position - transform.position).normalized*speed*Time.deltaTime;
            }
            else
            {
                walked = true;
                if(GetComponent<Animator>().GetBool("Walking"))
                {
                    GetComponent<Animator>().SetBool("Walking", false);
                }
            }
        }
        else if(walked)
        {
            Vector3 toLook = new Vector3(block.transform.position.x, transform.position.y,block.transform.position.z);
            transform.LookAt(toLook);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (talkable)
            {
                this.gameObject.GetComponent<NPCPay>().enabled = true;
                dialogueSystem.EnterRangeOfNPC();
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (talkable && !deactivated)
        {
            if ((other.CompareTag("Player")) && Input.GetKeyDown(KeyCode.F))
            {
                deactivated = true;
              if(done)
              {
                    dialogueSystem.dialogueLines = sentencesDone;
              }
              else
              {
                if(CollectiblesManager.instance.howManyPoints() >= payment)
                {
                    dialogueSystem.dialogueLines = sentencesMoney;
                    CollectiblesManager.instance.spendPoints(payment);
                    block.SetActive(false);
                    done = true;
                    PlayerPrefs.SetFloat("PlanetariumDone",1);
                }
                else
                {
                    dialogueSystem.dialogueLines = sentencesNoMoney;
                }
              }
                dialogueSystem.NPCName(false);
            }
           this.gameObject.GetComponent<NPCPay>().enabled = true;

        }
    }

    public void triggerDialogue(){
             if(done)
              {
                    dialogueSystem.dialogueLines = sentencesDone;
                     Debug.Log("ALLEN DONE");

              }
              else
              {
                
                if(CollectiblesManager.instance.howManyPoints() >= payment)
                {
                    dialogueSystem.dialogueLines = sentencesMoney;
                    CollectiblesManager.instance.spendPoints(payment);
                    block.SetActive(false);
                    done = true;
                }
                else
                {
                    dialogueSystem.dialogueLines = sentencesNoMoney;
                                         Debug.Log("ALLEN POOR");

                }
              }
        dialogueSystem.NPCName(true);
    }

    public void OnTriggerExit()
    {
        dialogueSystem.setTextNotEnabled();
    }
}

