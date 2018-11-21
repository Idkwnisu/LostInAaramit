using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{

    public GameObject boss;
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;
    public GameObject note5;
    public GameObject note6;
    public GameObject centralPlat;

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
        cLevel[1] = 7;
        cLevel[2] = 10;

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
        level2[4] = note2;
        level2[5] = note1;
        level2[6] = note5;

        level3 = new GameObject[cLevel[2]];
        level3[0] = note3;
        level3[1] = note6;
        level3[2] = note1;
        level3[3] = note4;
        level3[4] = note2;
        level3[5] = note1;
        level3[6] = note5;
        level3[7] = note2;
        level3[8] = note1;
        level3[9] = note5;

        boss.GetComponent<NPC>().triggerDialogue();
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
                        startNewLevel(level3);
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
                        Debug.Log("BOSS SCONFITTO");
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
        yield return new WaitForSeconds(6f);
        for (int i = 0; i < lev.Length; i++)
        {
            lev[i].GetComponent<Renderer>().material = right;
            yield return new WaitForSeconds(1.5f);
            lev[i].GetComponent<Renderer>().material = default_mat;
        }

    }

    IEnumerator LightNote(GameObject note, Material mat)
    {
        note.GetComponent<Renderer>().material = mat;
        yield return new WaitForSeconds(2f);
        note.GetComponent<Renderer>().material = default_mat;
    }
}
