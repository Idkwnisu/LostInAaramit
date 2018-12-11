using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{

    public GameObject boss;
    /* Dialogue
    public GameObject bossDialogue1;
    public GameObject bossDialogue2;
    public GameObject bossDialogue3;
    public GameObject bossDialogue4;
    */
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
        nLevels = 3;
        cLevel = new int[nLevels];
        cLevel[0] = 4;
        cLevel[1] = 6;
        cLevel[2] = 6;

        level1 = new GameObject[cLevel[0]];
        level1[0] = note1;
        level1[1] = note2;
        level1[2] = note6;
        level1[3] = note3;

        level2 = new GameObject[cLevel[1]];
        level2[0] = note3;
        level2[1] = note6;
        level2[2] = note1;
        level2[3] = note4;
        level2[4] = note6;
        level2[5] = note5;

        level3 = new GameObject[cLevel[2]];
        level3[0] = note1;
        level3[1] = note2;
        level3[2] = note3;
        level3[3] = note4;
        level3[4] = note5;
        level3[5] = note6;

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
                        startBossMove();
                    }
                }
                else
                {
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
                        //bossDialogue4.GetComponent<NPC>().triggerDialogue();
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
        /* Dialogue
        yield return new WaitForSeconds(1f);
        //Player.GetComponent<PlayerControllerRun>().DisableControl();
      
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

        }*/
    yield return new WaitForSeconds(5f);
        for (int i = 0; i < lev.Length; i++)
        {
            lev[i].transform.GetChild(0).GetComponent<Renderer>().material = right;
            yield return new WaitForSeconds(1.5f);
            lev[i].transform.GetChild(0).GetComponent<Renderer>().material = default_mat;
        }
        //Player.GetComponent<PlayerControllerRun>().EnableControl();
    }

    IEnumerator LightNote(GameObject note, Material mat)
    {
        note.transform.GetChild(0).GetComponent<Renderer>().material = mat;
        yield return new WaitForSeconds(2f);
        note.transform.GetChild(0).GetComponent<Renderer>().material = default_mat;
    }

    private void startBossMove(){
        boss.GetComponent<BossMovement>().setActive();
    }
}
