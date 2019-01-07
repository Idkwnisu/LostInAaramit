using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWhistle : MonoBehaviour {

    public AudioClip[] SoundToPlay;
    public float Volume;
    public int timer;
    private bool enable;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        enable = false;
        audio = GetComponent<AudioSource>();
    }

    public void startPlay(){
        enable = true;
        StartCoroutine(PlaySound());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(PlaySound());
        }

    }


    IEnumerator PlaySound()
    {
        while (true)
        {
            int rand = Random.Range(0, SoundToPlay.Length);
            yield return new WaitForSeconds(timer);

            audio.PlayOneShot(SoundToPlay[rand], Volume);
        }
    }
}
