using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWhistle : MonoBehaviour {

    public AudioClip SoundToPlay;
    public float Volume;
    public int timer;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();

        StartCoroutine(PlaySound());
    }


    IEnumerator PlaySound()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            audio.PlayOneShot(SoundToPlay, Volume);
        }
    }
}
