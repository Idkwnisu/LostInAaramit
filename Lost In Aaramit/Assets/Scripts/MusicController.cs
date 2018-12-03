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

    public AdjustTrackPitch controller1; 
    public AdjustTrackPitch controller2;
    public AdjustTrackPitch controller3;
    public AdjustTrackPitch controller4;
    public AdjustTrackPitch controller5;

    public bool returnToFullTrack = true;

    public int segment;

    private bool _win;

    public void SelectSegment()
    {
        //Debug.Log("MC IN AZIONE");
        switch (segment)
        {
            case 1:
                Song.clip = Seg2;
                Song.pitch = Seg2_pitch;
                segment = 2;
                Song.Play();
                break;
            case 2:
                Song.clip = Seg3;
                Song.pitch = Seg3_pitch;
                segment = 3;
                Song.Play();
                break;
            case 3:
                Song.clip = Seg4;
                Song.pitch = Seg4_pitch;
                segment = 4;
                Song.Play();
                break;
            case 4:
                Song.clip = Seg5;
                Song.pitch = Seg5_pitch;
                segment = 5;
                Song.Play();
                break;
            case 5:
                Song.clip = Seg1;
                Song.pitch = Seg1_pitch;
                segment = 1;
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
            returnToFullTrack = true;
            Song.Play();
        }
    }

    public void Update()
    {
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
        if (Seg1_pitch < 1.01 && Seg1_pitch > 0.99 && Seg2_pitch < 1.01 && Seg2_pitch > 0.99 && Seg3_pitch < 1.01 &&
            Seg3_pitch > 0.99 && Seg4_pitch < 1.01 && Seg4_pitch > 0.9 && Seg5_pitch < 1.01 && Seg5_pitch > 0.9 &&
            controller1.clip.name == "Seg1" && controller2.clip.name == "Seg2" && controller3.clip.name == "Seg3" && 
            controller4.clip.name == "Seg4" && controller5.clip.name == "Seg5" && _win == false)
        {
            //Debug.Log("WIN");
            winSoundEffect.Play();
            segment = 5;
            _win = true;
        }
    }
}