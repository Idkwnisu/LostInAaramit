using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MusicController : MonoBehaviour
{
    public AudioSource Song;
    public AudioSource soundEffect;

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

    public SimpleHealthBar pitchProgressBar;
    public SimpleHealthBar reorderingProgressBar;
    public float pitchProgress;
    public float reorderingProgress;

    public int segment;

    public bool _win=false;
    private bool initDone;

    public Material lumen;
    public Material normal;

    public NPC crazyPup;
    public NPC HelpfulPup;

    public void SelectSegment()
    {
        //Debug.Log("MC IN AZIONE");
        switch (segment)
        {
            case 1:
                Song.clip = Seg2;
                Song.pitch = Seg2_pitch;
                segment = 2;
                ChangeMaterial(controller2,lumen);
                ChangeMaterial(controller1, normal);
                Song.Play();
                break;
            case 2:
                Song.clip = Seg3;
                Song.pitch = Seg3_pitch;
                segment = 3;
                ChangeMaterial(controller3, lumen);
                ChangeMaterial(controller2, normal);
                Song.Play();
                break;
            case 3:
                Song.clip = Seg4;
                Song.pitch = Seg4_pitch;
                segment = 4;
                ChangeMaterial(controller4, lumen);
                ChangeMaterial(controller3, normal);
                Song.Play();
                break;
            case 4:
                Song.clip = Seg5;
                Song.pitch = Seg5_pitch;
                segment = 5;
                ChangeMaterial(controller5, lumen);
                ChangeMaterial(controller4, normal);
                Song.Play();
                break;
            case 5:
                Song.clip = Seg1;
                Song.pitch = Seg1_pitch;
                segment = 1;
                ChangeMaterial(controller1, lumen);
                ChangeMaterial(controller5, normal);
                Song.Play();
                break;
            default: break;
        }
    }

    public void Start()
    {
        _win = false;
        if (Song.isPlaying == false)
        {
            Song.clip = Seg1;
            Song.pitch = Seg1_pitch;
            segment = 1;
            ChangeMaterial(controller1, lumen);
            returnToFullTrack = true;
            Song.Play();
        }
        pitchProgressBar.UpdateBar(0.0f, 5.0f);
        reorderingProgressBar.UpdateBar(0.0f, 5.0f);
        pitchProgress = 0f;
        reorderingProgress = 0f;
        initDone = false;
        HelpfulPup.GetComponent<Animator>().SetBool("Stopped", true);
    }

    public void Update()
    {
        if (!initDone)
        {
            controller1.CheckCorrectness(2);
            controller2.CheckCorrectness(2);
            controller3.CheckCorrectness(2);
            controller4.CheckCorrectness(2);
            controller5.CheckCorrectness(2);
            initDone = true;
        }

        //Debug.Log(""+returnToFullTrack);
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
        if (Song.isPlaying == false && returnToFullTrack == true){
            SelectSegment();
        }
        if ((int)pitchProgress == 5 && (int)reorderingProgress == 5 && _win == false)
        {
            //Debug.Log("WIN");
            soundEffect.clip = winSoundEffect;
            soundEffect.Play();
            segment = 5;
            _win = true;
            crazyPup.sentences.SetValue("You did it!",0);
            crazyPup.sentences.SetValue("Now you can take the Feather of Virtue.", 1);
            crazyPup.sentences.SetValue("Good luck!", 2);
        }
    }

    public void PlayerIsInteracting(SongPlayer cube, bool on)
    {
        if (cube.simplePlayer)
        {
            cube.displaySegment.segment = 2;
            cube.displaySegment.display = false;
            cube.displaySegment.square1.image.color = Color.white;
            cube.displaySegment.square2.image.color = Color.black;
            cube.displaySegment.square3.image.color = Color.black;
            cube.displaySegment.square4.image.color = Color.black;
            cube.displaySegment.square5.image.color = Color.black;
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
                Song.pitch = cube.pitch;
                Song.clip = cube.clip;
                Song.Play();
            }
            else
            {
                Song.pitch = 1.0f;
                cube.displaySegment.display = true;
                Song.clip = cube.displaySegment.Seg1;
                Song.Play();
            }
        }
        else
        {
            ChangeMaterial(cube, normal);
            ChangeMaterial(controller1, normal);

            segment = 5;
            Song.Stop();
            SelectSegment();
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
        soundEffect.clip = successSoundEffect;
        soundEffect.Play();
    }
}