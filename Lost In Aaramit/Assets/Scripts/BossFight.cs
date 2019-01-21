﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{

    public GameObject cloudBoss;
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
    public GameObject nablaBoss;

    public GameObject Allen;
    public GameObject Player;

    public Camera CameraBoss;
    public Camera Camera;

    public Material right;
    public Material wrong;
    public Material default_mat;

    private GameObject[] level1, level2, level3;
    private int[] cLevel;
    private int nLevels;

    private float bossVelocity = 0;
    private int cNote = 0;
    private int cLev = 0;
    private bool ended;

    public Transform checkPlayer;
    public Transform checkNablaPlayer;
    public Transform checkNablaBoss;

    void Start()
    {
        ended = false;
        Camera.enabled = true;
        CameraBoss.enabled = false;

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

        if (PlayerPrefs.GetString("BossStarted").Equals("1"))
        {
            Player.transform.position = checkPlayer.transform.position;
            centralPlat.transform.position = checkNablaPlayer.transform.position;
            nablaBoss.transform.position = checkNablaBoss.transform.position;
            Vector3 posB = checkNablaBoss.position + new Vector3(0, 1f, 0);
            boss.transform.position = posB;
            centralPlat.GetComponent<NablaBossMove>().yetEnter = true;
            centralPlat.GetComponent<NablaBossMove>().setNotActive();
        } else {
            centralPlat.GetComponent<NablaBossMove>().yetEnter = false;
        }
    }

    private void Update()
    {
        /*
        Vector3 playerPos = Player.transform.position;
        boss.transform.LookAt(transform.position);
        */
        if(ended && Input.GetKeyDown(KeyCode.V)){
            string sceneName = PlayerPrefs.GetString("lastLoadedScene");
            PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName);
        }
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
                        bossVelocity = 3f;
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
                        bossVelocity = 5f;
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
                        /*
                        stepText.enabled = true;
                        stepText.text = "Boss sconfitto, premere ESC";
                        */
                        stopBossMove();
                        endBoss();
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
        stopBossMove();
        yield return new WaitForSeconds(1f);
        /*
        stepText.enabled = true;
        int levStep = cLev + 1;
        stepText.text = "Step " + levStep;
        */
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
        yield return new WaitForSeconds(5.5f);
        Allen.GetComponent<PlayerControllerRun>().ControlDisablingPermanent();
        Camera.enabled = !Camera.enabled;
        CameraBoss.enabled = !CameraBoss.enabled;
        stepText.enabled = false;
        for (int i = 0; i < lev.Length; i++)
        {
            lev[i].transform.GetComponent<Renderer>().material = right;
            yield return new WaitForSeconds(1.5f);
            lev[i].transform.GetComponent<Renderer>().material = default_mat;
        }
        yield return new WaitForSeconds(0.0f);
        Allen.GetComponent<PlayerControllerRun>().ControlEnabling();
        startBossMove(bossVelocity);
        Camera.enabled = !Camera.enabled;
        CameraBoss.enabled = !CameraBoss.enabled;
    }

    IEnumerator LightNote(GameObject note, Material mat)
    {
        note.transform.GetComponent<Renderer>().material = mat;
        yield return new WaitForSeconds(2f);
        note.transform.GetComponent<Renderer>().material = default_mat;
    }


    private void startBossMove(float speed){
        cloudBoss.GetComponent<BossMovement>().setActive(speed);
    }

    private void stopBossMove()
    {
        cloudBoss.GetComponent<BossMovement>().setNotActive();
    }

    private void endBoss()
    {
        ended = true;
        stepText.enabled = true;
        stepText.text = "Press V to return to the Village";
    }
}
