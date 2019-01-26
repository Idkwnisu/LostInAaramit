using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVillage : MonoBehaviour {

    public AudioClip musicV;

    public AudioClip musicB;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.changeMusicSound(musicV);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.changeMusicSound(musicB);

        }
    }
}
