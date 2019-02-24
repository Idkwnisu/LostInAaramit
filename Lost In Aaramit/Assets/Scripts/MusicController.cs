using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MusicController : MonoBehaviour
{
    public AudioClip Seg1;
    public AudioClip Seg2;
    public AudioClip Seg3;
    public AudioClip Seg4;
    public AudioClip Seg5;
    public AudioClip winSoundEffect;
    public AudioClip successSoundEffect;

    public float Seg1_pitch = 1.0f;
    public float Seg2_pitch = 1.0f;
    public float Seg3_pitch = 1.0f;
    public float Seg4_pitch = 1.0f;
    public float Seg5_pitch = 1.0f;

    public SongPlayer controller1; 
    public SongPlayer controller2;
    public SongPlayer controller3;
    public SongPlayer controller4;
    public SongPlayer controller5;
    public SongPlayer songPlayer;

    public bool returnToFullTrack = true;

    public float pitchProgress;
    public float reorderingProgress;

    public int segment;

    public bool _win=false;
    private bool initDone;

    public Material lumen;
    public Material normal;

    public NPC crazyPup;
    public NPC HelpfulPup;

    public GameObject magicEffect;

    private bool pause = false;
    private bool playerIsInteracting = false;
    public bool triggerWin = true;

    public AudioManager audioManager;

    public void Start()
    {
        _win = false;
        segment = 1;
        ChangeMaterial(controller1, lumen);
        returnToFullTrack = true;
        pitchProgress = 0f;
        reorderingProgress = 0f;
        initDone = false;
        HelpfulPup.GetComponent<Animator>().SetBool("Stopped", true);
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.musicSource.loop = false;
    }

    public void Update()
    {
        if (!initDone)
        {
            audioManager.changeMusicSound(Seg1);
            audioManager.musicSource.pitch = Seg1_pitch;
            controller1.CheckCorrectness(2);
            controller2.CheckCorrectness(2);
            controller3.CheckCorrectness(2);
            controller4.CheckCorrectness(2);
            controller5.CheckCorrectness(2);
            initDone = true;
        }
        Seg1 = controller1.clip;
        Seg2 = controller2.clip;
        Seg3 = controller3.clip;
        Seg4 = controller4.clip;
        Seg5 = controller5.clip;
        Seg1_pitch = controller1.pitch;
        Seg2_pitch = controller2.pitch;
        Seg3_pitch = controller3.pitch;
        Seg4_pitch = controller4.pitch;
        Seg5_pitch = controller5.pitch;
        if (audioManager.musicSource.isPlaying == false && returnToFullTrack == true && !pause)
        {
            if (!_win)
            {
                pause = true;
            }
            Debug.Log("ENTRATO");
            StartCoroutine(PauseSong());
        }
        if ((int)pitchProgress == 5 && (int)reorderingProgress == 5 && _win == false)
        {
            audioManager.PlaySingle(winSoundEffect, 0.7f);
            segment = 5;
            _win = true;
            triggerWin = true;
            magicEffect.SetActive(false);
            crazyPup.GetComponent<NPC>().triggerDialogue();
        }
    }

    public void PlayerIsInteracting(SongPlayer cube, bool on)
    {
        playerIsInteracting = true;
        if (cube.simplePlayer)
        {
            cube.displaySegment.segment = 2;
            cube.displaySegment.display = false;
            cube.displaySegment.square1.image.color = cube.displaySegment.lumen.color;
            cube.displaySegment.square2.image.color = cube.displaySegment.normal.color;
            cube.displaySegment.square3.image.color = cube.displaySegment.normal.color;
            cube.displaySegment.square4.image.color = cube.displaySegment.normal.color;
            cube.displaySegment.square5.image.color = cube.displaySegment.normal.color;
        }
        if (on == true){
            ChangeMaterial(controller1, normal);
            ChangeMaterial(controller2, normal);
            ChangeMaterial(controller3, normal);
            ChangeMaterial(controller4, normal);
            ChangeMaterial(controller5, normal);
            ChangeMaterial(cube, lumen);

            returnToFullTrack = false;
            if (!cube.simplePlayer)
            {
                audioManager.musicSource.pitch = cube.pitch;
                audioManager.changeMusicSound(cube.clip);
            }
            else
            {
                audioManager.musicSource.pitch = 1.0f;
                cube.displaySegment.playerIsInteracting = true;
                cube.displaySegment.display = true;
                audioManager.changeMusicSound(cube.displaySegment.Seg1);
            }
        }
        else
        {
            if (cube.simplePlayer)
            {
                cube.displaySegment.playerIsInteracting = false;
            }
            playerIsInteracting = false;
            ChangeMaterial(cube, normal);
            ChangeMaterial(controller1, normal);
            segment = 5;
            audioManager.musicSource.Stop();
            StartCoroutine(PauseSong());
            returnToFullTrack = true;
        }
    }

    private void ChangeMaterial(SongPlayer controller, Material newMat)
    {
        Renderer[] children;
        children = controller.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMat;
            }
            rend.materials = mats;
        }
    }

    public void playNoteSoundEffect()
    {
        audioManager.PlaySingle(successSoundEffect, 0.7f);
    }

    IEnumerator PauseSong()
    {
        if (pause)
        {
            yield return new WaitForSeconds((float)0.5);
        }
        if (!playerIsInteracting)
        {
            switch (segment)
            {
                case 1:
                    audioManager.musicSource.pitch = Seg2_pitch;
                    audioManager.changeMusicSound(Seg2);
                    segment = 2;
                    ChangeMaterial(controller2, lumen);
                    ChangeMaterial(controller1, normal);
                    break;
                case 2:
                    audioManager.musicSource.pitch = Seg3_pitch;
                    audioManager.changeMusicSound(Seg3);
                    segment = 3;
                    ChangeMaterial(controller3, lumen);
                    ChangeMaterial(controller2, normal);
                    break;
                case 3:
                    audioManager.musicSource.pitch = Seg4_pitch;
                    audioManager.changeMusicSound(Seg4);
                    segment = 4;
                    ChangeMaterial(controller4, lumen);
                    ChangeMaterial(controller3, normal);
                    break;
                case 4:
                    audioManager.musicSource.pitch = Seg5_pitch;
                    audioManager.changeMusicSound(Seg5);
                    segment = 5;
                    ChangeMaterial(controller5, lumen);
                    ChangeMaterial(controller4, normal);
                    break;
                case 5:
                    audioManager.musicSource.pitch = Seg1_pitch;
                    audioManager.changeMusicSound(Seg1);
                    segment = 1;
                    ChangeMaterial(controller1, lumen);
                    ChangeMaterial(controller5, normal);
                    break;
                default:
                    break;
            }
        }
        pause = false;
    }
}