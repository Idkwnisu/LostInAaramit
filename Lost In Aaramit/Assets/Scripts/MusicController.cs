using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MusicController : MonoBehaviour
{
    public AudioSource Song;
    public AudioSource winSoundEffect;

    public AudioClip Seg1;
    public AudioClip Seg2;
    public AudioClip Seg3;
    public AudioClip Seg4;
    public AudioClip Seg5;

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

    public int scene;
    //0 = complete; 1 = pitch; 2 = reoredering

    public int segment;

    private bool _win;
    private bool initDone;

    public Material lumen;
    public Material normal;

    public void SelectSegment()
    {
        //Debug.Log("MC IN AZIONE");
        switch (segment)
        {
            case 1:
                Song.clip = Seg2;
                Song.pitch = Seg2_pitch;
                segment = 2;
                controller2.GetComponent<Renderer>().material = lumen;
                controller1.GetComponent<Renderer>().material = normal;
                Song.Play();
                break;
            case 2:
                Song.clip = Seg3;
                Song.pitch = Seg3_pitch;
                segment = 3;
                controller3.GetComponent<Renderer>().material = lumen;
                controller2.GetComponent<Renderer>().material = normal;
                Song.Play();
                break;
            case 3:
                Song.clip = Seg4;
                Song.pitch = Seg4_pitch;
                segment = 4;
                controller4.GetComponent<Renderer>().material = lumen;
                controller3.GetComponent<Renderer>().material = normal;
                Song.Play();
                break;
            case 4:
                Song.clip = Seg5;
                Song.pitch = Seg5_pitch;
                segment = 5;
                controller5.GetComponent<Renderer>().material = lumen;
                controller4.GetComponent<Renderer>().material = normal;
                Song.Play();
                break;
            case 5:
                Song.clip = Seg1;
                Song.pitch = Seg1_pitch;
                segment = 1;
                controller1.GetComponent<Renderer>().material = lumen;
                controller5.GetComponent<Renderer>().material = normal;
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
            controller1.GetComponent<Renderer>().material = lumen;
            returnToFullTrack = true;
            Song.Play();
        }
        pitchProgressBar.UpdateBar(0.0f, 5.0f);
        reorderingProgressBar.UpdateBar(0.0f, 5.0f);
        pitchProgress = 0f;
        reorderingProgress = 0f;
        initDone = false;
    }

    public void Update()
    {
        if (!initDone)
        {
            controller1.CheckPitch();
            controller1.CheckReordering();
            controller2.CheckPitch();
            controller2.CheckReordering();
            controller3.CheckPitch();
            controller3.CheckReordering();
            controller4.CheckPitch();
            controller4.CheckReordering();
            controller5.CheckPitch();
            controller5.CheckReordering();
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
            winSoundEffect.Play();
            segment = 5;
            _win = true;
        }
    }

    public void PlayerIsInteracting(SongPlayer cube, bool on)
    {
        if (on == true){
            controller1.GetComponent<Renderer>().material = normal;
            controller2.GetComponent<Renderer>().material = normal;
            controller3.GetComponent<Renderer>().material = normal;
            controller4.GetComponent<Renderer>().material = normal;
            controller5.GetComponent<Renderer>().material = normal;
            cube.GetComponent<Renderer>().material = lumen;

            returnToFullTrack = false;
            Song.pitch = cube.pitch;
            Song.clip = cube.clip;
            Song.Play();
        }
        else
        {
            cube.GetComponent<Renderer>().material = normal;
            controller1.GetComponent<Renderer>().material = lumen;

            segment = 5;
            Song.Stop();
            SelectSegment();
            returnToFullTrack = true;
        }
    }
}