using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{

    public GameObject boss;
    /* Dialogue */
    public GameObject bossDialogue1;
    public GameObject bossDialogue2;
    public GameObject bossDialogue3;
    public GameObject bossDialogue4;
    public Text stepText;
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;
    public GameObject note5;
    public GameObject note6;
    public GameObject centralPlat;

    public GameObject Player;

    public Material right;
    public Material wrong;
    public Material default_mat;

    private GameObject[] level1, level2, level3;
    private int[] cLevel;
    private int nLevels;

    private int cNote = 0;
    private int cLev = 0;

    void Start()
    {

        stepText.enabled = false;

        nLevels = 3;
        cLevel = new int[nLevels];
        cLevel[0] = 4;
        cLevel[1] = 3;
        cLevel[2] = 4;

        level1 = new GameObject[cLevel[0]];
        level1[0] = note1;
        level1[1] = note2;
        level1[2] = note6;
        level1[3] = note3;

        level2 = new GameObject[cLevel[1]];
        level2[0] = note4;
        level2[1] = note6;
        level2[2] = note2;

        level3 = new GameObject[cLevel[2]];
        level3[0] = note3;
        level3[1] = note6;
        level3[2] = note4;
        level3[3] = note3;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartFight();
    }

    public void StartFight()
    {
        StartCoroutine(startNewLevel(level1));
    }

    public void UpdateLevel(GameObject note)
    {
        switch (cLev)
        {
            case 0:
                if (ReferenceEquals(level1[cNote], note))
                {
                    StartCoroutine(LightNote(note, right));
                    cNote++;
                    if (cNote == cLevel[cLev])
                    {
                        cLev++;
                        cNote = 0;
                        StartCoroutine(startNewLevel(level2));
                        startBossMove(4f);
                    }
                }
                else
                {
                    StartCoroutine(LightNote(note, wrong));
                    note.GetComponent<Rigidbody>().isKinematic = false;
                }
                break;
            case 1:
                if (ReferenceEquals(level2[cNote], note))
                {
                    StartCoroutine(LightNote(note, right));
                    cNote++;
                    if (cNote == cLevel[cLev])
                    {
                        cLev++;
                        cNote = 0;
                        StartCoroutine(startNewLevel(level3));
                        startBossMove(6f);
                    }
                }
                else
                {
                    stopBossMove();
                    StartCoroutine(LightNote(note, wrong));
                    note.GetComponent<Rigidbody>().isKinematic = false;
                }
                break;
            case 2:
                if (ReferenceEquals(level3[cNote], note))
                {
                    StartCoroutine(LightNote(note, right));
                    cNote++;
                    if (cNote == cLevel[cLev])
                    {
                        cLev++;
                        cNote = 0;
                        stepText.enabled = true;
                        stepText.text = "Boss sconfitto, premere ESC";
                        bossDialogue4.GetComponent<NPC>().triggerDialogue();
                    }
                }
                else
                {
                    StartCoroutine(LightNote(note, wrong));
                    note.GetComponent<Rigidbody>().isKinematic = false;
                }
                break;
        }
    }

    IEnumerator startNewLevel(GameObject[] lev)
    {
       
        yield return new WaitForSeconds(1f);
        stepText.enabled = true;
        int levStep = cLev + 1;
        stepText.text = "Step " + levStep;
        /* Dialogue*/
      
        switch (cLev){
            case 0:
                bossDialogue1.GetComponent<NPC>().triggerDialogue();
                break;
            case 1:
                bossDialogue2.GetComponent<NPC>().triggerDialogue();
                break;
            case 2:
                bossDialogue3.GetComponent<NPC>().triggerDialogue();
                break;

        }
        yield return new WaitForSeconds(5f);
        stepText.enabled = false;
        for (int i = 0; i < lev.Length; i++)
        {
            lev[i].transform.GetChild(0).GetComponent<Renderer>().material = right;
            yield return new WaitForSeconds(1.5f);
            lev[i].transform.GetChild(0).GetComponent<Renderer>().material = default_mat;
        }
    }

    IEnumerator LightNote(GameObject note, Material mat)
    {
        note.transform.GetChild(0).GetComponent<Renderer>().material = mat;
        yield return new WaitForSeconds(2f);
        note.transform.GetChild(0).GetComponent<Renderer>().material = default_mat;
    }

    private void startBossMove(float speed){
        boss.GetComponent<BossMovement>().setActive(speed);
    }

    private void stopBossMove()
    {
        boss.GetComponent<BossMovement>().setNotActive();
    }
}
