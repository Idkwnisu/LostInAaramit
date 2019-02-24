using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource musicSource;
    public static AudioManager instance = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    public float efxVolumeMultiplier = 1.0f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        /*else if (instance != this)
            Destroy(gameObject);*/
        DontDestroyOnLoad(gameObject);
        if (GameObject.FindGameObjectsWithTag("AudioManager").Length > 1)
            Destroy(gameObject);
    }

    public void changeMusicSound(AudioClip track){
        musicSource.clip = track;
        musicSource.Play();
    }

    public void PlaySingle(AudioClip clip, float efxVolume)
    {
        if (efxVolumeMultiplier >= 1.01f)
        {
            efxVolume += (1 - efxVolume) * (efxVolumeMultiplier - 1);
        }
        else if (efxVolumeMultiplier <= 0.99f)
        {
            efxVolume -= efxVolume * (1 - efxVolumeMultiplier);
        }
        efxSource.volume = efxVolume;
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];

        efxSource.Play();
    }

    public void DestroyAudioManager()
    {
        Destroy(gameObject);
    }
}
